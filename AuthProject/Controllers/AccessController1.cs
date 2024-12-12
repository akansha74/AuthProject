using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using AuthProject.Models;

namespace AuthProject.Controllers
{
    public class AccessController1 : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult>Login(VMLogin modelLogin)
            
        {
            if (modelLogin.Email== "akansha314@gmail.com" && modelLogin.PassWord=="aka123")
            {
                List<Claim> claims = new List<Claim>()
                {
                    new Claim (ClaimTypes.NameIdentifier,modelLogin.Email),
                    new Claim("OtherProperties","Example Role")
                };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);
                AuthenticationProperties properties = new AuthenticationProperties()
                {
                    AllowRefresh = true,
                    IsPersistent=modelLogin.KeepLoggedIn
                };
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), properties);
                return RedirectToAction("Index", "Home");
            }
            ViewData["ValidateMessage"] = "user not found";
            return View();
        }
    }
}
