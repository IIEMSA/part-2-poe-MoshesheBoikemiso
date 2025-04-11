using Microsoft.AspNetCore.Mvc;
using CLDV6211_WebApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace CLDV6211_WebApplication.Controllers
{
    public class EventController : Controller
    {
        private readonly WebAppDBContext _context;

        public EventController(WebAppDBContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var events = await _context.Events.ToListAsync();
            return View(events);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Event ev)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ev);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ev);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            var ev = await _context.Events.FirstOrDefaultAsync(e => e.EventID == id);
            if (ev == null)
            {
                return NotFound();
            }
            return View(ev);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ev = await _context.Events.FindAsync(id);
            _context.Events.Remove(ev);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
