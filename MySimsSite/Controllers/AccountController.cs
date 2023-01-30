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
                    db.Users.Add(new User { Login = model.Login, Password = model.Password });
                    await db.SaveChangesAsync();

                    await Authenticate(model.Login);
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