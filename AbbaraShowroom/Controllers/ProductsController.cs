using AbbaraShowroom.Context;
using AbbaraShowroom.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AbbaraShowroom.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ProductsController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // Ürün listesi
        public async Task<IActionResult> Index()
        {
            var products = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Photos)
                .ToListAsync();

            return View(products);
        }

        // Yeni ürün formu
        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name");
            return View();
        }

        // Ürün kaydetme
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product, List<IFormFile> photos)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Add(product);
                await _context.SaveChangesAsync();

                if (photos != null && photos.Any())
                {
                    foreach (var photo in photos)
                    {
                        if (photo.Length > 0)
                        {
                            var fileName = Guid.NewGuid() + Path.GetExtension(photo.FileName);
                            var uploadPath = Path.Combine(_env.WebRootPath, "img/uploads", fileName);

                            using (var stream = new FileStream(uploadPath, FileMode.Create))
                            {
                                await photo.CopyToAsync(stream);
                            }

                            _context.ProductPhotos.Add(new ProductPhoto
                            {
                                ProductId = product.Id,
                                ImagePath = fileName
                            });
                        }
                    }
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
            return View(product);
        }

        // Ürün düzenleme formu
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var product = await _context.Products
                .Include(p => p.Photos)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null) return NotFound();

            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
            return View(product);
        }

        // Ürün düzenleme işlemi
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Product product)
        {
            if (id != product.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Products.Any(p => p.Id == id))
                        return NotFound();
                    throw;
                }
            }

            ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
            return View(product);
        }

        // AJAX üzerinden silme
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products
                .Include(p => p.Photos)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
                return Json(new { success = false, message = "Ürün bulunamadı." });

            // Fotoğrafları klasörden sil
            foreach (var photo in product.Photos)
            {
                var filePath = Path.Combine(_env.WebRootPath, "img/uploads", photo.ImagePath);
                if (System.IO.File.Exists(filePath))
                    System.IO.File.Delete(filePath);

                _context.ProductPhotos.Remove(photo);
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }
    }
}