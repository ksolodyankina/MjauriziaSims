﻿using Domain.Abstract;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using MjauriziaSims.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.Net.Mail;
using System.Net;
using Microsoft.AspNetCore.Authentication.Google;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration.UserSecrets;
using System.Security.Principal;

namespace MjauriziaSims.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly MessageManager.MessageManager _msgManager;
        private readonly IEnumerable<Pack> _packs;
        private readonly IUserPackRepository _userPackRepository;
        public AccountController(
            IUserRepository userRepository, 
            MessageManager.MessageManager msgManager,
            IPackRepository packRepository,
            IUserPackRepository userPackRepository)
        {
            _userRepository = userRepository;
            _msgManager = msgManager;
            _packs = packRepository.Packs.ToList();
            _userPackRepository = userPackRepository;
        }

        [HttpPost]
        public async Task<Result> Login(LoginModel model)
        {
            var result = new Result() {IsSuccess = true};
            if (ModelState.IsValid)
            {
                var pass = EncryptPassword(model.Password, model.Email);
                var user = _userRepository.Users.FirstOrDefault(u => u.Email == model.Email && u.Password == pass);
                if (user != null)
                {
                    if (user.IsActive)
                    {
                        await Authenticate(user);
                    }
                    else
                    {
                        result.IsSuccess = false;
                        result.ErrorMsg = _msgManager.Msg("err_emailNotConfirmed");
                    }
                }
                else
                {
                    result.IsSuccess = false;
                    result.ErrorMsg = _msgManager.Msg("err_incorrectPass");
                }
            }
            return result;
        }

        public async Task GoogleLogin()
        {
            await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme, new AuthenticationProperties()
            {
                RedirectUri = Url.Action("GoogleResponse")
            });
        }

        public async Task<IActionResult> GoogleResponse()
        {
            await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            var email = User.FindFirst(ClaimTypes.Email).Value;
            var user = _userRepository.Users.FirstOrDefault(u => u.Email == email);
            if (user == null)
            {
                var name = User.FindFirst(ClaimTypes.Name).Value;
                user = new User
                {
                    Username = name,
                    Email = email,
                    Role = Roles.User,
                    IsActive = true
                };
                _userRepository.SaveUser(user);
            }
            else if (user.Password != "")
            {
                await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                return Redirect("/Account/LoginFail");
            }

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await Authenticate(user);

            return Redirect("/");
        }

        [HttpGet]
        public ViewResult Registration()
        {
            var newUser = new User();
            var model = new RegisterModel()
            {
                Role = newUser.Role,
                Packs = _packs.Select(p => p.PackId).ToList(),
                PackRepository = _packs,
                MsgManager = _msgManager
            };
            return View(model);
        }

        [HttpPost]
        public async Task<Result> Registration(RegisterModel model)
        {
            var result = new Result() { IsSuccess = true };
            if (ModelState.IsValid)
            {
                User user = _userRepository.Users.FirstOrDefault(u => u.Email == model.Email);
                if (user == null)
                {
                    var token = Guid.NewGuid();
                    _userRepository.SaveUser(new User
                    {
                        Username = model.Username, 
                        Email = model.Email, 
                        Password = EncryptPassword(model.Password, model.Email), 
                        ConfirmationToken = token, 
                        Role=model.Role
                    });
                    var newUser = _userRepository.Users.Last(u => u.Email == model.Email).UserId;
                    _userPackRepository.SaveUserPacks(newUser, model.Packs);

                    var emailText = _msgManager.Msg("registrationText").
                            Replace("<url>", $"https://mjauriziasims.ru/Account/Confirmation?ConfirmationToken={token}");
                    SendEmail(new EmailInformation(model.Email, _msgManager.Msg("registrationSubject"), emailText));
                }
                else if (user.Password != "")
                {
                    result.IsSuccess = false;
                    result.ErrorMsg = _msgManager.Msg("err_registrationEmail");
                }
                else
                {
                    result.IsSuccess = false;
                    result.ErrorMsg = _msgManager.Msg("err_registrationGoogle");
                }
            }
            else
            {
                result.IsSuccess = false;
                var allErrors = ModelState.Values.SelectMany(v => v.Errors);
                result.ErrorMsg = String.Join(". ", allErrors.Select(v => v.ErrorMessage));
            }
            return result;
        }


        [HttpGet]
        public IActionResult Edit()
        {
            var user = int.Parse(User.FindFirst("UserId").Value);
            var model = new UserEditModel()
            {
                UserId = user,
                Username = _userRepository.Users.First(u => u.UserId ==  user).Username,
                Packs = _userPackRepository.UserPacks.Where(u => u.UserId == user).Select(u => u.PackId).ToList(),
                PackRepository = _packs,
                MsgManager = _msgManager
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(UserEditModel model)
        {
            var user = _userRepository.Users.First(u => u.UserId == model.UserId);
            if (user.Username != model.Username)
            {
                user.Username = model.Username;
                _userRepository.SaveUser(user);
            }
            _userPackRepository.SaveUserPacks(model.UserId, model.Packs);

            return Redirect("/Account/Edit/");
        }

        [HttpGet]
        public async Task<IActionResult> Confirmation(Guid ConfirmationToken)
        {
            var isSuccess = false;
            var user = _userRepository.Users.FirstOrDefault(u => u.ConfirmationToken == ConfirmationToken);
            if (user != null)
            {
                isSuccess = true;
                user.IsActive = true;
                _userRepository.SaveUser(user);
                await Authenticate(user);
            }

            return View(isSuccess);
        }
        
        [HttpGet]
        public async Task<IActionResult> Recovery()
        {
            return View();
        }

        [HttpPost]
        public async Task<Result> Recovery(RecoveryModel model)
        {
            var result = new Result() { IsSuccess = true };
            if (ModelState.IsValid)
            {
                var user = _userRepository.Users.FirstOrDefault(u => u.Email == model.Email);
                if (user == null)
                {
                    result.IsSuccess = false;
                    result.ErrorMsg = _msgManager.Msg("err_recovery");
                }
                else if (user.Password == "")
                {
                    result.IsSuccess = false;
                    result.ErrorMsg = _msgManager.Msg("err_recoveryGoogle");
                }
                else
                {
                    var token = Guid.NewGuid();

                    user.ConfirmationToken = token;
                    _userRepository.SaveUser(user);

                    var emailText = _msgManager.Msg("recoveryText").
                            Replace("<url>", $"https://mjauriziasims.ru/Account/ResetPassword?ConfirmationToken={token}");
                    SendEmail(new EmailInformation(user.Email, _msgManager.Msg("recoverySubject"), emailText));
                }

            }
            else
            {
                result.IsSuccess = false;
                var allErrors = ModelState.Values.SelectMany(v => v.Errors);
                result.ErrorMsg = String.Join(". ", allErrors.Select(v => v.ErrorMessage));
            }
            return result;
        }


        [HttpGet]
        public async Task<IActionResult> ResetPassword(String ConfirmationToken)
        {
            var model = new ResetPassModel();
            model.ConfirmationToken = Guid.Parse(ConfirmationToken);

            var user = _userRepository.Users.FirstOrDefault(u => u.ConfirmationToken == Guid.Parse(ConfirmationToken));
            if (user != null)
            {
                model.UserId = user.UserId;
            }

            return View(model);
        }

        [HttpPost]
        public async Task<Result> ResetPassword(ResetPassModel model)
        {
            var result = new Result() { IsSuccess = true };
            if (ModelState.IsValid)
            {
                var user = _userRepository.Users.FirstOrDefault
                    (u => u.UserId == model.UserId && u.ConfirmationToken == model.ConfirmationToken);
                if (user == null)
                {
                    result.IsSuccess = false;
                    result.ErrorMsg = _msgManager.Msg("err_PassReset");
                }
                else
                {
                    user.Password = EncryptPassword(model.Password, user.Email);
                    _userRepository.SaveUser(user);
                }

            }
            else
            {
                result.IsSuccess = false;
                var allErrors = ModelState.Values.SelectMany(v => v.Errors);
                result.ErrorMsg = String.Join(". ", allErrors.Select(v => v.ErrorMessage));
            }
            return result;
        }

        [HttpGet]
        public async Task<IActionResult> ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<Result> ChangePassword(ChangePassModel model)
        {
            var result = new Result() { IsSuccess = true };
            if (ModelState.IsValid)
            {
                var user = _userRepository.Users.FirstOrDefault
                    (u => u.UserId == int.Parse(User.FindFirst("UserId").Value));
                if (user.Password != EncryptPassword(model.OldPassword, user.Email))
                {
                    result.IsSuccess = false;
                    result.ErrorMsg = _msgManager.Msg("err_ChangePass");
                }
                else
                {
                    user.Password = EncryptPassword(model.Password, user.Email);
                    _userRepository.SaveUser(user);
                }

            }
            else
            {
                result.IsSuccess = false;
                var allErrors = ModelState.Values.SelectMany(v => v.Errors);
                result.ErrorMsg = String.Join(". ", allErrors.Select(v => v.ErrorMessage));
            }
            return result;
        }
        private async Task Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Username),
                new Claim("UserId", user.UserId.ToString()),
                new Claim(ClaimTypes.Role, ((Roles)(user.Role)).ToString())
            };
            ClaimsIdentity id = new ClaimsIdentity
                (claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            var authProperties = new AuthenticationProperties();
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id), authProperties);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect(Request.Headers["Referer"].ToString());
        }

        public ViewResult LoginFail()
        {
            return View();
        }

        private void SendEmail(EmailInformation info)
        {
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("mjaurizia@gmail.com", "yfybujjmrdudaibe"),
                EnableSsl = true,
            };

            smtpClient.Send(
                "mjaurizia@gmail.com", 
                info.Email, 
                info.Subject, 
                info.Text);
        }

        public static string EncryptPassword(string password, string email)
        {
            var md5 = MD5.Create();
            var result =  md5.ComputeHash(Encoding.UTF8.GetBytes(password + email));
            return Convert.ToBase64String(result);
        }
    }

    public class Result
    {
        public bool IsSuccess { get; set; }
        public string ErrorMsg { get; set; }
    }

    public class EmailInformation
    {
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Text { get; set; }

        public EmailInformation(string email, string subject, string text)
        {
            Email = email;
            Subject = subject;
            Text = text;
        }
    }
}