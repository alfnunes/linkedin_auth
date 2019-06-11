using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LinkedinTestAPP.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");
            return View();
        }

        public async Task Login()
        {
            await Microsoft.AspNetCore.Authentication.AuthenticationHttpContextExtensions.ChallengeAsync(HttpContext, "LinkedIn", new Microsoft.AspNetCore.Authentication.AuthenticationProperties
            {
                RedirectUri = "/",
            });

        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await Microsoft.AspNetCore.Authentication.AuthenticationHttpContextExtensions.SignOutAsync(HttpContext, CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index");
        }
    }
}