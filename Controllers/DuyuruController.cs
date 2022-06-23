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
    public class DuyuruController : Controller
    {
        private readonly DbContext _context;

        public DuyuruController(DbContext context)
        {
            _context = context;
        }

        // GET: Duyuru
        public async Task<IActionResult> Index()
        {
              return View(await _context.Duyuru.ToListAsync());
        }

        // GET: Duyuru/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Duyuru == null)
            {
                return NotFound();
            }

            var duyuru = await _context.Duyuru
                .FirstOrDefaultAsync(m => m.DuyuruId == id);
            if (duyuru == null)
            {
                return NotFound();
            }

            return View(duyuru);
        }

        // GET: Duyuru/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Duyuru/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DuyuruId,DuyuruName,DuyuruText,DuyuruTarih")] Duyuru duyuru)
        {
            if (ModelState.IsValid)
            {
                _context.Add(duyuru);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(duyuru);
        }

        // GET: Duyuru/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Duyuru == null)
            {
                return NotFound();
            }

            var duyuru = await _context.Duyuru.FindAsync(id);
            if (duyuru == null)
            {
                return NotFound();
            }
            return View(duyuru);
        }

        // POST: Duyuru/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DuyuruId,DuyuruName,DuyuruText,DuyuruTarih")] Duyuru duyuru)
        {
            if (id != duyuru.DuyuruId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(duyuru);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DuyuruExists(duyuru.DuyuruId))
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
            return View(duyuru);
        }

        // GET: Duyuru/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Duyuru == null)
            {
                return NotFound();
            }

            var duyuru = await _context.Duyuru
                .FirstOrDefaultAsync(m => m.DuyuruId == id);
            if (duyuru == null)
            {
                return NotFound();
            }

            return View(duyuru);
        }

        // POST: Duyuru/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Duyuru == null)
            {
                return Problem("Entity set 'DbContext.Duyuru'  is null.");
            }
            var duyuru = await _context.Duyuru.FindAsync(id);
            if (duyuru != null)
            {
                _context.Duyuru.Remove(duyuru);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DuyuruExists(int id)
        {
          return _context.Duyuru.Any(e => e.DuyuruId == id);
        }
    }
}
