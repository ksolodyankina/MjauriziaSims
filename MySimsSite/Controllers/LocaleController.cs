using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace MjauriziaSims.Controllers
{
    public class LocaleController : Controller
    {
        [HttpGet]
        public void SetLocale(string culture)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );;
        }
    }
}
