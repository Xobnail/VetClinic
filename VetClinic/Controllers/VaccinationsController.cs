
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VetClinic.Data;
using VetClinic.Models;

namespace VetClinic.Controllers
{
    public class VaccinationsController : Controller
    {
        private readonly VetClinicContext _context;

        public VaccinationsController(VetClinicContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var vetClinicContext = _context.Vaccinations.Include(v => v.Animal);
            return View(await vetClinicContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Vaccinations == null)
            {
                return NotFound();
            }

            var vaccination = await _context.Vaccinations
                .Include(v => v.Animal)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vaccination == null)
            {
                return NotFound();
            }

            return View(vaccination);
        }

        public IActionResult Create()
        {
            ViewData["AnimalId"] = new SelectList(_context.Animals, "Id", "Id");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,DateTime,AnimalId,Description")] Vaccination vaccination)
        {
            _context.Add(vaccination);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Vaccinations == null)
            {
                return NotFound();
            }

            var vaccination = await _context.Vaccinations.FindAsync(id);
            if (vaccination == null)
            {
                return NotFound();
            }
            ViewData["AnimalId"] = new SelectList(_context.Animals, "Id", "Id", vaccination.AnimalId);
            return View(vaccination);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,DateTime,AnimalId,Description")] Vaccination vaccination)
        {
            if (id != vaccination.Id)
            {
                return NotFound();
            }

            try
            {
                _context.Update(vaccination);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VaccinationExists(vaccination.Id))
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

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Vaccinations == null)
            {
                return NotFound();
            }

            var vaccination = await _context.Vaccinations
                .Include(v => v.Animal)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vaccination == null)
            {
                return NotFound();
            }

            return View(vaccination);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Vaccinations == null)
            {
                return Problem("Entity set 'VetClinicContext.Vaccinations'  is null.");
            }
            var vaccination = await _context.Vaccinations.FindAsync(id);
            if (vaccination != null)
            {
                _context.Vaccinations.Remove(vaccination);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VaccinationExists(int id)
        {
          return (_context.Vaccinations?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
