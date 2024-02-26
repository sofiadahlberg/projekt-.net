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
    public class TimeInterValController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TimeInterValController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TimeInterVal
        public async Task<IActionResult> Index()
        {
            return View(await _context.TimeIntervals.ToListAsync());
        }

        // GET: TimeInterVal/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timeInterval = await _context.TimeIntervals
                .FirstOrDefaultAsync(m => m.TimeIntervalId == id);
            if (timeInterval == null)
            {
                return NotFound();
            }

            return View(timeInterval);
        }

        // GET: TimeInterVal/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TimeInterVal/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TimeIntervalId,StartTime,EndTime,Available,ApiKeyRequired")] TimeInterval timeInterval)
        {
            if (ModelState.IsValid)
            {
                _context.Add(timeInterval);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(timeInterval);
        }

        // GET: TimeInterVal/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timeInterval = await _context.TimeIntervals.FindAsync(id);
            if (timeInterval == null)
            {
                return NotFound();
            }
            return View(timeInterval);
        }

        // POST: TimeInterVal/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TimeIntervalId,StartTime,EndTime,Available,ApiKeyRequired")] TimeInterval timeInterval)
        {
            if (id != timeInterval.TimeIntervalId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(timeInterval);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TimeIntervalExists(timeInterval.TimeIntervalId))
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
            return View(timeInterval);
        }

        // GET: TimeInterVal/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var timeInterval = await _context.TimeIntervals
                .FirstOrDefaultAsync(m => m.TimeIntervalId == id);
            if (timeInterval == null)
            {
                return NotFound();
            }

            return View(timeInterval);
        }

        // POST: TimeInterVal/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var timeInterval = await _context.TimeIntervals.FindAsync(id);
            if (timeInterval != null)
            {
                _context.TimeIntervals.Remove(timeInterval);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TimeIntervalExists(int id)
        {
            return _context.TimeIntervals.Any(e => e.TimeIntervalId == id);
        }
    }
}
