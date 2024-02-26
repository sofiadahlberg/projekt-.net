using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BookingApp.Models;
using TreatmentApp.Data;
using Microsoft.AspNetCore.Authorization;

namespace TreatmentApp.Controllers
{
    [Authorize]
    public class TreatmentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TreatmentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Treatment
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Treatments.Include(t => t.Customer).Include(t => t.TimeInterval);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Treatment/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var treatment = await _context.Treatments
                .Include(t => t.Customer)
                .Include(t => t.TimeInterval)
                .FirstOrDefaultAsync(m => m.TreatmentId == id);
            if (treatment == null)
            {
                return NotFound();
            }

            return View(treatment);
        }

        // GET: Treatment/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "Lastname");
            ViewData["TimeIntervalId"] = new SelectList(_context.TimeIntervals, "TimeIntervalId", "TimeIntervalId");
            return View();
        }

        // POST: Treatment/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TreatmentId,Duration,Category,ApiKeyRequired,CustomerId,TimeIntervalId")] Treatment treatment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(treatment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "Lastname", treatment.CustomerId);
            ViewData["TimeIntervalId"] = new SelectList(_context.TimeIntervals, "TimeIntervalId", "TimeIntervalId", treatment.TimeIntervalId);
            return View(treatment);
        }

        // GET: Treatment/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var treatment = await _context.Treatments.FindAsync(id);
            if (treatment == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "Lastname", treatment.CustomerId);
            ViewData["TimeIntervalId"] = new SelectList(_context.TimeIntervals, "TimeIntervalId", "TimeIntervalId", treatment.TimeIntervalId);
            return View(treatment);
        }

        // POST: Treatment/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TreatmentId,Duration,Category,ApiKeyRequired,CustomerId,TimeIntervalId")] Treatment treatment)
        {
            if (id != treatment.TreatmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(treatment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TreatmentExists(treatment.TreatmentId))
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
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "Lastname", treatment.CustomerId);
            ViewData["TimeIntervalId"] = new SelectList(_context.TimeIntervals, "TimeIntervalId", "TimeIntervalId", treatment.TimeIntervalId);
            return View(treatment);
        }

        // GET: Treatment/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var treatment = await _context.Treatments
                .Include(t => t.Customer)
                .Include(t => t.TimeInterval)
                .FirstOrDefaultAsync(m => m.TreatmentId == id);
            if (treatment == null)
            {
                return NotFound();
            }

            return View(treatment);
        }

        // POST: Treatment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var treatment = await _context.Treatments.FindAsync(id);
            if (treatment != null)
            {
                _context.Treatments.Remove(treatment);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TreatmentExists(int id)
        {
            return _context.Treatments.Any(e => e.TreatmentId == id);
        }
    }
}
