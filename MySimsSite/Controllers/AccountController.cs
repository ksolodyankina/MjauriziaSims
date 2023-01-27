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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await db.Users.FirstOrDefaultAsync(u => u.Login == model.Login && u.Password == model.Password);
                if (user != null)
                {
                    await Authenticate(model.Login); // аутентификация
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return Redirect(Request.Headers["Referer"].ToString());
        }

        [HttpGet]
        public ViewResult Registration()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registration(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await db.Users.FirstOrDefaultAsync(u => u.Login == model.Login && u.Password == model.Password);
                if (user == null)
                {
                    db.Users.Add(new User { Login = model.Login, Password = model.Password });
                    await db.SaveChangesAsync();

                    await Authenticate(model.Login);

                    return Redirect("/");
                }
                else
                    ModelState.AddModelError("", "Uncorrect login or password");
            }
            return RedirectToAction("Registration", "Account");
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
}