using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace Travel.Web.Controllers
{
    public class LanguageController : Controller
    {
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SetLanguage(
            string culture,
            string returnUrl)
        {
            if (culture != "tr-TR" &&
                culture != "en-US")
            {
                culture = "tr-TR";
            }

            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(
                    new RequestCulture(culture)),
                new CookieOptions
                {
                    Expires =
                        DateTimeOffset.UtcNow.AddYears(1),

                    IsEssential = true
                });

            if (!string.IsNullOrWhiteSpace(returnUrl) &&
                Url.IsLocalUrl(returnUrl))
            {
                return LocalRedirect(returnUrl);
            }

            return RedirectToAction(
                "Index",
                "Home");
        }
    }
}