using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LibraryWebApplication.Models;

namespace LibraryWebApplication.Controllers
{
    public class CarServicesController : Controller
    {
        private readonly DblibraryContext _context;

        public CarServicesController(DblibraryContext context)
        {
            _context = context;
        }

        // GET: CarServices
        public async Task<IActionResult> Index(int? id, string? name)
        {
            if(id == null) return RedirectToAction("Cities","Index");
            //var dblibraryContext = _context.CarServices.Include(c => c.City);
            //return View(await dblibraryContext.ToListAsync());

            ViewBag.CityId = id;
            ViewBag.CityName = name;
            var carServicesByCity = _context.CarServices.Where(b => b.CityId == id).Include(b => b.City);

            return View(await carServicesByCity.ToListAsync());
        }

        // GET: CarServices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CarServices == null)
            {
                return NotFound();
            }

            var carService = await _context.CarServices
                .Include(c => c.City)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carService == null)
            {
                return NotFound();
            }

            //return View(carService);
            return RedirectToAction("Index", "Services", new { id = carService.Id, name = carService.Name});
        }

        // GET: CarServices/Create
        public IActionResult Create(int cityId)
        {
            //ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Name");
            ViewBag.CityId = cityId;
            ViewBag.CityName = _context.Cities.Where(c => c.Id == cityId).FirstOrDefault().Name;
            return View();
        }

        // POST: CarServices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int cityId, [Bind("Id,Address,Name,Information")] CarService carService)
        {
            carService.CityId = cityId;

            if (ModelState.IsValid)
            {
                _context.Add(carService);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Index", "CarServices", new {id = cityId, name = _context.Cities.Where(c => c.Id == cityId).FirstOrDefault().Name});
            }
            //ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Name", carService.CityId);
            //return View(carService);
            return RedirectToAction("Index", "CarServices", new { id = cityId, name = _context.Cities.Where(c => c.Id == cityId).FirstOrDefault().Name });
        }

        // GET: CarServices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CarServices == null)
            {
                return NotFound();
            }

            var carService = await _context.CarServices.FindAsync(id);
            if (carService == null)
            {
                return NotFound();
            }
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Name", carService.CityId);
            return View(carService);
        }

        // POST: CarServices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Address,Name,Information,CityId")] CarService carService)
        {
            if (id != carService.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carService);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarServiceExists(carService.Id))
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
            ViewData["CityId"] = new SelectList(_context.Cities, "Id", "Name", carService.CityId);
            return View(carService);
        }

        // GET: CarServices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CarServices == null)
            {
                return NotFound();
            }

            var carService = await _context.CarServices
                .Include(c => c.City)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carService == null)
            {
                return NotFound();
            }

            return View(carService);
        }

        // POST: CarServices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CarServices == null)
            {
                return Problem("Entity set 'DblibraryContext.CarServices'  is null.");
            }
            var carService = await _context.CarServices.FindAsync(id);
            if (carService != null)
            {
                _context.CarServices.Remove(carService);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarServiceExists(int id)
        {
          return (_context.CarServices?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
