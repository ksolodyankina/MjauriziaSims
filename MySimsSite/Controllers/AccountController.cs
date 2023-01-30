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

namespace MjauriziaSims.Controllers
{
    public class AccountController : Controller
    {
        private EFDbContext db;
        public AccountController(EFDbContext context)
        {
            db = context;
        }

        [HttpPost]
        public async Task<Result> Login(LoginModel model)
        {
            var result = new Result() {IsSuccess = true};
            if (ModelState.IsValid)
            {
                User user = await db.Users.FirstOrDefaultAsync(u => u.Login == model.Login && u.Password == model.Password);
                if (user != null)
                {
                    await Authenticate(model.Login);
                }
                else
                {
                    result.IsSuccess = false;
                    result.ErrorMsg = "Incorrect login or password";
                }
            }
            return result;
        }

        [HttpGet]
        public ViewResult Registration()
        {
            return View();
        }

        [HttpPost]
        public async Task<Result> Registration(RegisterModel model)
        {
            var result = new Result() { IsSuccess = true };
            if (ModelState.IsValid)
            {
                User user = await db.Users.FirstOrDefaultAsync(u => u.Login == model.Login && u.Password == model.Password);
                if (user == null)
                {
                    var token = Guid.NewGuid();
                    db.Users.Add(new User { Login = model.Login, Email = model.Email, Password = model.Password, ConfirmationToken = token});
                    await db.SaveChangesAsync();

                    var smtpClient = new SmtpClient("smtp.gmail.com")
                    {
                        Port = 587,
                        Credentials = new NetworkCredential("ks.solodyankina@gmail.com", "xnzbtoydlqtocnov"),
                        EnableSsl = true,
                    };

                    var emailText = $"To confirm your email follow the link:\n https://localhost:7029/Account/Confirmation?ConfirmationToken={token}";
                    smtpClient.Send("ks.solodyankina@gmail.com", model.Email, "Registration at MjauriziaSims", emailText);
                }
                else
                {
                    result.IsSuccess = false;
                    result.ErrorMsg = "User with this login already exists";
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
            var user = await db.Users.FirstOrDefaultAsync(u => u.ConfirmationToken == ConfirmationToken);
            if (user != null)
            {
                isSuccess = true;
                user.IsActive = true;
                await db.SaveChangesAsync();
                await Authenticate(user.Login);
            }

            return View(isSuccess);
        }

        private async Task Authenticate(string userName)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            ClaimsIdentity id = new ClaimsIdentity
                (claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }

    public class Result
    {
        public bool IsSuccess { get; set; }
        public string ErrorMsg { get; set; }
    }
}