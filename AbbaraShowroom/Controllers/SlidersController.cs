using AbbaraShowroom.Context;
using AbbaraShowroom.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AbbaraShowroom.Controllers
{
    [Authorize]
    public class SlidersController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SlidersController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // Slider listesi
        public async Task<IActionResult> Index()
        {
            var sliders = await _context.Sliders.OrderByDescending(s => s.Id).ToListAsync();
            return View(sliders);
        }

        // Yeni slider formu
        public IActionResult Create()
        {
            return View();
        }

        // Yeni slider ekleme
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Slider slider, IFormFile imageFile)
        {
            if (ModelState.IsValid)
            {
                if (imageFile != null && imageFile.Length > 0)
                {
                    var fileName = Guid.NewGuid() + Path.GetExtension(imageFile.FileName);
                    var uploadPath = Path.Combine(_env.WebRootPath, "img/uploads", fileName);

                    using (var stream = new FileStream(uploadPath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }

                    slider.ImagePath = fileName;
                }

                _context.Sliders.Add(slider);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(slider);
        }

        // Güncelleme formu
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var slider = await _context.Sliders.FindAsync(id);
            if (slider == null) return NotFound();

            return View(slider);
        }

        // Güncelleme işlemi
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Slider slider, IFormFile? imageFile)
        {
            if (id != slider.Id) return NotFound();

            if (ModelState.IsValid)
            {
                var existingSlider = await _context.Sliders.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id);
                if (existingSlider == null) return NotFound();

                if (imageFile != null && imageFile.Length > 0)
                {
                    var oldPath = Path.Combine(_env.WebRootPath, "img/uploads", existingSlider.ImagePath);
                    if (System.IO.File.Exists(oldPath))
                        System.IO.File.Delete(oldPath);

                    var fileName = Guid.NewGuid() + Path.GetExtension(imageFile.FileName);
                    var uploadPath = Path.Combine(_env.WebRootPath, "img/uploads", fileName);

                    using (var stream = new FileStream(uploadPath, FileMode.Create))
                    {
                        await imageFile.CopyToAsync(stream);
                    }

                    slider.ImagePath = fileName;
                }
                else
                {
                    slider.ImagePath = existingSlider.ImagePath;
                }

                _context.Update(slider);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(slider);
        }

        // Silme (AJAX ile)
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var slider = await _context.Sliders.FindAsync(id);
            if (slider == null)
                return Json(new { success = false, message = "Slider bulunamadı." });

            var filePath = Path.Combine(_env.WebRootPath, "img/uploads", slider.ImagePath);
            if (System.IO.File.Exists(filePath))
                System.IO.File.Delete(filePath);

            _context.Sliders.Remove(slider);
            await _context.SaveChangesAsync();

            return Json(new { success = true });
        }
    }
}
