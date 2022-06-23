using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EduProject.Models;

namespace EduProject.Controllers
{
    public class DersController : Controller
    {
        private readonly DbContext _context;

        public DersController(DbContext context)
        {
            _context = context;
        }

        // GET: Ders
        public async Task<IActionResult> Index()
        {
              return _context.Ders != null ? 
                          View(await _context.Ders.ToListAsync()) :
                          Problem("Entity set 'DbContext.Ders'  is null.");
        }

        // GET: Ders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Ders == null)
            {
                return NotFound();
            }

            var ders = await _context.Ders
                .FirstOrDefaultAsync(m => m.DersId == id);
            if (ders == null)
            {
                return NotFound();
            }

            return View(ders);
        }

        // GET: Ders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DersId,DersKodu,Name")] Ders ders)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ders);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ders);
        }

        // GET: Ders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Ders == null)
            {
                return NotFound();
            }

            var ders = await _context.Ders.FindAsync(id);
            if (ders == null)
            {
                return NotFound();
            }
            return View(ders);
        }

        // POST: Ders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DersId,DersKodu,Name")] Ders ders)
        {
            if (id != ders.DersId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ders);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DersExists(ders.DersId))
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
            return View(ders);
        }

        // GET: Ders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Ders == null)
            {
                return NotFound();
            }

            var ders = await _context.Ders
                .FirstOrDefaultAsync(m => m.DersId == id);
            if (ders == null)
            {
                return NotFound();
            }

            return View(ders);
        }

        // POST: Ders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Ders == null)
            {
                return Problem("Entity set 'DbContext.Ders'  is null.");
            }
            var ders = await _context.Ders.FindAsync(id);
            if (ders != null)
            {
                _context.Ders.Remove(ders);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DersExists(int id)
        {
          return (_context.Ders?.Any(e => e.DersId == id)).GetValueOrDefault();
        }
    }
}
