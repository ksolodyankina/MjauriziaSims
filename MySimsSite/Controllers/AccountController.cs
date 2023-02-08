using Domain.Abstract;
using Domain.Entities;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebUI.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Domain.Concrete;
using Microsoft.EntityFrameworkCore;
using MjauriziaSims.Models;
using System.Security.Policy;
using Microsoft.AspNetCore.Components.RenderTree;
using System.Net.Mail;
using System.Net;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Google;
using Azure;
using Azure.Core;

namespace MjauriziaSims.Controllers
{
    public class AccountController : Controller
    {
        private IUserRepository _userRepository;
        private MessageManager.MessageManager _msgManager;
        public AccountController(
            IUserRepository userRepository, 
            MessageManager.MessageManager msgManager)
        {
            _userRepository = userRepository;
            _msgManager = msgManager;
        }

        [HttpPost]
        public async Task<Result> Login(LoginModel model)
        {
            var result = new Result() {IsSuccess = true};
            if (ModelState.IsValid)
            {
                User user = _userRepository.Users.FirstOrDefault(u => u.Login == model.Login && u.Password == model.Password);
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

        [HttpGet]
        public ViewResult Registration()
        {
            var newUser = new User();
            var model = new RegisterModel()
            {
                Role = newUser.Role
            };
            return View(model);
        }

        [HttpPost]
        public async Task<Result> Registration(RegisterModel model)
        {
            var result = new Result() { IsSuccess = true };
            if (ModelState.IsValid)
            {
                User user = _userRepository.Users.FirstOrDefault(u => u.Login == model.Login && u.Password == model.Password);
                if (user == null)
                {
                    var token = Guid.NewGuid();
                    _userRepository.SaveUser(new User
                    {
                        Login = model.Login, 
                        Email = model.Email, 
                        Password = model.Password, 
                        ConfirmationToken = token, 
                        Role=model.Role
                    });

                    var emailText = _msgManager.Msg("registrationText").
                            Replace("<url>", $"https://localhost:7029/Account/Confirmation?ConfirmationToken={token}");
                    SendEmail(new EmailInformation(model.Email, _msgManager.Msg("registrationSubject"), emailText));
                }
                else
                {
                    result.IsSuccess = false;
                    result.ErrorMsg = _msgManager.Msg("err_registration");
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
                else
                {
                    var token = Guid.NewGuid();

                    user.ConfirmationToken = token;
                    _userRepository.SaveUser(user);

                    var emailText = _msgManager.Msg("recoveryText").
                            Replace("<url>", $"https://localhost:7029/Account/ResetPassword?ConfirmationToken={token}");
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
                    user.Password = model.Password;
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
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Login),
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

        private void SendEmail(EmailInformation info)
        {
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("ks.solodyankina@gmail.com", "xnzbtoydlqtocnov"),
                EnableSsl = true,
            };

            smtpClient.Send(
                "ks.solodyankina@gmail.com", 
                info.Email, 
                info.Subject, 
                info.Text);
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