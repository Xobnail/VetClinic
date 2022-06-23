using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VetClinic.Data;
using VetClinic.Models;

namespace VetClinic.Controllers
{
    public class AnimalsController : Controller
    {
        private readonly VetClinicContext _context;

        public AnimalsController(VetClinicContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var vetClinicContext = _context.Animals.Include(a => a.Doctor).Include(a => a.Owner);
            return View(await vetClinicContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Animals == null)
            {
                return NotFound();
            }

            var animal = await _context.Animals
                .Include(a => a.Doctor)
                .Include(a => a.Owner)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (animal == null)
            {
                return NotFound();
            }

            return View(animal);
        }

        public IActionResult Create()
        {
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "Id", "Id");
            ViewData["OwnerId"] = new SelectList(_context.Owners, "Id", "Id");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,NickName,EnterDate,OwnerId,DoctorId,DiseaseDescription")] Animal animal)
        {
            Doctor doctor = _context.Doctors.FirstOrDefault(a => a.Id == animal.DoctorId);
            Owner owner = _context.Owners.FirstOrDefault(a => a.Id == animal.OwnerId);

            animal.Doctor = doctor;
            animal.Owner = owner;

            _context.SaveChanges();

            _context.Add(animal);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Animals == null)
            {
                return NotFound();
            }

            var animal = await _context.Animals.FindAsync(id);
            if (animal == null)
            {
                return NotFound();
            }
            ViewData["DoctorId"] = new SelectList(_context.Doctors, "Id", "Id", animal.DoctorId);
            ViewData["OwnerId"] = new SelectList(_context.Owners, "Id", "Id", animal.OwnerId);
            return View(animal);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,NickName,EnterDate,OwnerId,DoctorId,DiseaseDescription")] Animal animal)
        {
            if (id != animal.Id)
            {
                return NotFound();
            }

            Doctor doctor = _context.Doctors.FirstOrDefault(a => a.Id == animal.DoctorId);
            Owner owner = _context.Owners.FirstOrDefault(a => a.Id == animal.OwnerId);

            animal.Doctor = doctor;
            animal.Owner = owner;

            _context.SaveChanges();

            try
            {
                _context.Update(animal);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnimalExists(animal.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));

            ViewData["DoctorId"] = new SelectList(_context.Doctors, "Id", "Id", animal.DoctorId);
            ViewData["OwnerId"] = new SelectList(_context.Owners, "Id", "Id", animal.OwnerId);
            return View(animal);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Animals == null)
            {
                return NotFound();
            }

            var animal = await _context.Animals
                .Include(a => a.Doctor)
                .Include(a => a.Owner)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (animal == null)
            {
                return NotFound();
            }

            return View(animal);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Animals == null)
            {
                return Problem("Entity set 'VetClinicContext.Animals'  is null.");
            }
            var animal = await _context.Animals.FindAsync(id);
            if (animal != null)
            {
                _context.Animals.Remove(animal);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnimalExists(int id)
        {
          return (_context.Animals?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
