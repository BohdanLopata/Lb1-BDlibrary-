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
    public class ServicesController : Controller
    {
        private readonly DblibraryContext _context;

        public ServicesController(DblibraryContext context)
        {
            _context = context;
        }

        // GET: Services
        public async Task<IActionResult> Index(int? id, string? name)
        {
            //var dblibraryContext = _context.Services.Include(s => s.Car).Include(s => s.CarService).Include(s => s.Worker);
            //return View(await dblibraryContext.ToListAsync());
            if (id == null) return RedirectToAction("CarServices", "Index");
            ViewBag.CarServicesId = id;
            ViewBag.CarServicesName = name;
            var serviceByCarServices = _context.Services.Where(b => b.CarServiceId == id).Include(b => b.CarService);

            return View(await serviceByCarServices.ToListAsync());
        }

        // GET: Services/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Services == null)
            {
                return NotFound();
            }

            var service = await _context.Services
                .Include(s => s.Car)
                .Include(s => s.CarService)
                .Include(s => s.Worker)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        // GET: Services/Create
        public IActionResult Create()
        {
            ViewData["CarId"] = new SelectList(_context.Cars, "Id", "Id");
            ViewData["CarServiceId"] = new SelectList(_context.CarServices, "Id", "Address");
            ViewData["WorkerId"] = new SelectList(_context.Workers, "Id", "Id");
            return View();
        }

        // POST: Services/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,StartDate,Deadline,Information,WorkerId,CarServiceId,CarId,Price")] Service service)
        {
            if (ModelState.IsValid)
            {
                _context.Add(service);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CarId"] = new SelectList(_context.Cars, "Id", "Id", service.CarId);
            ViewData["CarServiceId"] = new SelectList(_context.CarServices, "Id", "Address", service.CarServiceId);
            ViewData["WorkerId"] = new SelectList(_context.Workers, "Id", "Id", service.WorkerId);
            return View(service);
        }

        // GET: Services/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Services == null)
            {
                return NotFound();
            }

            var service = await _context.Services.FindAsync(id);
            if (service == null)
            {
                return NotFound();
            }
            ViewData["CarId"] = new SelectList(_context.Cars, "Id", "Id", service.CarId);
            ViewData["CarServiceId"] = new SelectList(_context.CarServices, "Id", "Address", service.CarServiceId);
            ViewData["WorkerId"] = new SelectList(_context.Workers, "Id", "Id", service.WorkerId);
            return View(service);
        }

        // POST: Services/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,StartDate,Deadline,Information,WorkerId,CarServiceId,CarId,Price")] Service service)
        {
            if (id != service.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(service);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServiceExists(service.Id))
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
            ViewData["CarId"] = new SelectList(_context.Cars, "Id", "Id", service.CarId);
            ViewData["CarServiceId"] = new SelectList(_context.CarServices, "Id", "Address", service.CarServiceId);
            ViewData["WorkerId"] = new SelectList(_context.Workers, "Id", "Id", service.WorkerId);
            return View(service);
        }

        // GET: Services/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Services == null)
            {
                return NotFound();
            }

            var service = await _context.Services
                .Include(s => s.Car)
                .Include(s => s.CarService)
                .Include(s => s.Worker)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        // POST: Services/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Services == null)
            {
                return Problem("Entity set 'DblibraryContext.Services'  is null.");
            }
            var service = await _context.Services.FindAsync(id);
            if (service != null)
            {
                _context.Services.Remove(service);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServiceExists(int id)
        {
          return _context.Services.Any(e => e.Id == id);
        }
    }
}
