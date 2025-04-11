using CLDV6211_WebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CLDV6211_WebApplication.Controllers
{
    public class BookingController : Controller
    {
        private readonly WebAppDBContext _context;

        public BookingController(WebAppDBContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var Bookings = await _context.Bookings.ToListAsync();
            return View(Bookings);
        }

        public IActionResult Create()
        {
            ViewBag.VenueID = new SelectList(_context.Venues, "VenueID", "VenueName");
            ViewBag.EventID = new SelectList(_context.Events, "EventID", "EventName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Booking bookings)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bookings);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            
            ViewBag.VenueID = new SelectList(_context.Venues, "VenueID", "VenueName", bookings.VenueID);
            ViewBag.EventID = new SelectList(_context.Events, "EventID", "EventName", bookings.EventID);
            return View(bookings);
        }

        public async Task<IActionResult> Details(int? id)
        {
            var bookings = await _context.Bookings.FirstOrDefaultAsync(m => m.BookingId == id);
            if (bookings == null)
            {
                return NotFound();
            }
            return View(bookings);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            var bookings = await _context.Bookings.FirstOrDefaultAsync(m => m.BookingId == id);
            if (bookings == null)
            {
                return NotFound();
            }
            return View(bookings);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var bookings = await _context.Bookings.FindAsync(id);
            _context.Bookings.Remove(bookings);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookingExists(int id)
        {
            return _context.Bookings.Any(e => e.BookingId == id);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookings = await _context.Bookings.FindAsync(id);
            {
                if (id == null)
                {
                    return NotFound();
                }
                return View(bookings);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Booking bookings)
        {
            if (id != bookings.BookingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookings);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingExists(bookings.BookingId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(bookings);
        }


    }
}

