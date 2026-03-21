using AbbaraShowroom.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AbbaraShowroom.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly AppDbContext _context;

        public DashboardController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Sayı bilgileri
            ViewBag.TotalCategories = _context.Categories.Count();
            ViewBag.TotalProducts = _context.Products.Count();
            ViewBag.TotalSliders = _context.Sliders.Count();
            ViewBag.TotalMessages = _context.Messages.Count();
            ViewBag.UnreadMessages = _context.Messages.Count(m => !m.IsRead);

            // Son eklenen 8 ürün
            var recentProducts = _context.Products
                .Include(p => p.Category)
                .OrderByDescending(p => p.Id)
                .Take(8)
                .ToList();

            return View(recentProducts);
        }
    }
}
