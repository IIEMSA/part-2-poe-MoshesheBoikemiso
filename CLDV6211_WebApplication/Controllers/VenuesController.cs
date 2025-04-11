using Microsoft.AspNetCore.Mvc;
using CLDV6211_WebApplication.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CLDV6211_WebApplication.Controllers
{
    public class VenuesController : Controller
    {
        private readonly WebAppDBContext _context;

        public VenuesController(WebAppDBContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var Venue = await _context.Venues.ToListAsync();
            return View(Venue);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        
        public async Task<IActionResult> Create(Venue venue)
        {
            if (ModelState.IsValid)
            {
                _context.Add(venue);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(venue);
        }

    }
}
