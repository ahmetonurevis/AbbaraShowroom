using AbbaraShowroom.Context;
using AbbaraShowroom.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AbbaraShowroom.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;

        public AccountController(AppDbContext context)
        {
            _context = context;
        }

        // Login Sayfası (GET)
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // Login Kontrol (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string username, string password)
        {
            var user = _context.AdminUsers.FirstOrDefault(u =>
                u.Username == username && u.Password == password);

            if (user != null)
            {
                // Giriş başarılı → Cookie oluştur
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Username)
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return RedirectToAction("Index", "Dashboard"); // yönlendirilecek panel sayfası
            }

            ViewBag.Error = "Geçersiz kullanıcı adı veya şifre!";
            return View();
        }

        // Çıkış
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
