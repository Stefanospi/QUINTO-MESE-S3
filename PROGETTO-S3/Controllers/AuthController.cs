using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using PROGETTO_S3.Models;
using PROGETTO_S3.Services.Auth;
using System.Security.Claims;

namespace PROGETTO_S3.Controllers
{

    [Route("Auth")]
    public class AuthController : Controller
    {
        private readonly IUserService _userService;
        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("Login")]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost("Login")]
        [ValidateAntiForgeryToken]
                public async Task<IActionResult> Login(User user)
        {
            try
            {
                var existingUser = await _userService.Login(user);
                if (existingUser == null)
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                    return View(user);
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, existingUser.Username)
                };

                existingUser.Role.ForEach(r =>
                {
                    claims.Add(new Claim(ClaimTypes.Role, r.Name));
                });

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return RedirectToAction("ProductList", "Product");
            }
            catch (Exception ex)
            {
                return View(user);
            }
        }




        [HttpGet("Register")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost("Register")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(User user)
        {
            try
            {
                await _userService.Register(user);
                return RedirectToAction("Login", "Auth");

            }
            catch (Exception ex)
            {
                return View(User);
            }

        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Auth");
        }
    }
}
