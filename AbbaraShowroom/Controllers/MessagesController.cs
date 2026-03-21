using AbbaraShowroom.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AbbaraShowroom.Controllers
{
    [Authorize]
    public class MessagesController : Controller
    {
        private readonly AppDbContext _context;

        public MessagesController(AppDbContext context)
        {
            _context = context;
        }

        // Listeleme
        public async Task<IActionResult> Index()
        {
            var messages = await _context.Messages
                .OrderByDescending(m => m.SentDate)
                .ToListAsync();

            return View(messages);
        }

        // Detay sayfası
        public async Task<IActionResult> Detail(int id)
        {
            var message = await _context.Messages.FindAsync(id);
            if (message == null)
                return NotFound();

            // Eğer daha önce okunmadıysa, şimdi okundu olarak işaretle
            if (!message.IsRead)
            {
                message.IsRead = true;
                _context.Messages.Update(message);
                await _context.SaveChangesAsync();
            }

            return View(message);
        }

        // Okundu işaretleme 
        [HttpPost]
        public async Task<IActionResult> MarkAsRead(int id)
        {
            var message = await _context.Messages.FindAsync(id);
            if (message != null && !message.IsRead)
            {
                message.IsRead = true;
                _context.Messages.Update(message);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }
    }
}
