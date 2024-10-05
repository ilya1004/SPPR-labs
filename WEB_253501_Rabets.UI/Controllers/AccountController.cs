using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;
using WEB_253501_Rabets.UI.Models;
using WEB_253501_Rabets.UI.Services.Authorization;

namespace WEB_253501_Rabets.UI.Controllers;

public class AccountController : Controller
{
    public async Task Login()
    {
        await HttpContext.ChallengeAsync(OpenIdConnectDefaults.AuthenticationScheme, new AuthenticationProperties { RedirectUri = "https://localhost:7001" });
    }

    [HttpPost]
    public async Task Logout()
    {
        await HttpContext.SignOutAsync(
            CookieAuthenticationDefaults.AuthenticationScheme);
        await HttpContext.SignOutAsync(
            OpenIdConnectDefaults.AuthenticationScheme,
            new AuthenticationProperties { RedirectUri = Url.Action("Index", "Home") });
    }

    public IActionResult Register()
    {
        return View(new RegisterUserViewModel());
    }

    [HttpPost]
    [AutoValidateAntiforgeryToken]
    public async Task<IActionResult> Register(RegisterUserViewModel user, [FromServices] IAuthService authService)
    {
        if (ModelState.IsValid)
        {
            if (user == null)
            {
                return BadRequest();
            }

            var result = await authService.RegisterUserAsync(user.Email, user.Password, user.Avatar);
            if (result.Result)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }
        }
        return View(user);
    }
}