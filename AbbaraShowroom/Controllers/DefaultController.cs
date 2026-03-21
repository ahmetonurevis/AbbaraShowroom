using AbbaraShowroom.Context;
using AbbaraShowroom.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AbbaraShowroom.Controllers
{
    public class DefaultController : Controller
    {
        private readonly AppDbContext _context;

        public DefaultController(AppDbContext context)
        {
            _context = context;
        }

        // ANASAYFA: Slider + Öne Çıkan Ürünler
        public async Task<IActionResult> Index()
        {
            var viewModel = new HomePageViewModel
            {
                Sliders = await _context.Sliders
                    .Where(s => s.IsActive)
                    .OrderByDescending(s => s.Id)
                    .ToListAsync(),

                Products = await _context.Products
                    .Include(p => p.Photos)
                    .Include(p => p.Category)
                    .Where(p => p.IsFeatured)
                    .OrderByDescending(p => p.Id)
                    .Take(4)
                    .ToListAsync()
            };

            return View(viewModel);
        }

        // TÜM ÜRÜNLER (kategoriye göre filtreleme)
        public async Task<IActionResult> Products(string category)
        {
            var productsQuery = _context.Products
                .Include(p => p.Photos)
                .Include(p => p.Category)
                .AsQueryable();

            if (!string.IsNullOrEmpty(category))
            {
                productsQuery = productsQuery
                    .Where(p => p.Category.Name.ToLower() == category.ToLower());
            }

            var products = await productsQuery
                .OrderByDescending(p => p.Id)
                .ToListAsync();

            var categoryNames = await _context.Categories
                .OrderBy(c => c.Name)
                .Select(c => c.Name)
                .ToListAsync();

            ViewBag.Categories = categoryNames;
            ViewBag.SelectedCategory = category;

            return View(products);
        }

        // ÜRÜN DETAY
        public async Task<IActionResult> ProductDetail(int id)
        {
            var product = await _context.Products
                .Include(p => p.Photos)
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
                return NotFound();

            return View(product);
        }

        // İLETİŞİM GET
        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }

        // İLETİŞİM POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Contact(Message message)
        {
            if (ModelState.IsValid)
            {
                message.SentDate = DateTime.Now;
                _context.Messages.Add(message);
                await _context.SaveChangesAsync();

                TempData["Success"] = "Mesajınız başarıyla gönderildi. Teşekkür ederiz!";
                return RedirectToAction("Contact");
            }

            return View(message);
        }
    }
}
