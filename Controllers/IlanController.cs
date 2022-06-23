using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EduProject.Models;
using X.PagedList;
using X.PagedList.Mvc.Core;

namespace EduProject.Controllers
{
    public class IlanController : Controller
    {
        private readonly DbContext _context;

        public IlanController(DbContext context)
        {
            _context = context;
        }

        // GET: Ilan
        public IActionResult Index(int? page)
        {
            var pageSize = 4;
            var pageNumber = page ?? 1;
            return View(_context.Ilan.ToPagedList(pageNumber, pageSize));
        }

        // GET: Ilan/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Ilan == null)
            {
                return NotFound();
            }

            var ilan = await _context.Ilan
                .FirstOrDefaultAsync(m => m.IlanId == id);
            if (ilan == null)
            {
                return NotFound();
            }

            return View(ilan);
        }

        // GET: Ilan/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ilan/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IlanId,Arıyor,Message,iletisim")] Ilan ilan)
        {
            if (ModelState.IsValid)
            {
                ilan.atCreated=DateTime.Now;
                _context.Add(ilan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ilan);
        }

        // GET: Ilan/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Ilan == null)
            {
                return NotFound();
            }

            var ilan = await _context.Ilan.FindAsync(id);
            if (ilan == null)
            {
                return NotFound();
            }
            return View(ilan);
        }

        // POST: Ilan/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IlanId,Arıyor,Message")] Ilan ilan)
        {
            if (id != ilan.IlanId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ilan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IlanExists(ilan.IlanId))
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
            return View(ilan);
        }

        // GET: Ilan/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Ilan == null)
            {
                return NotFound();
            }

            var ilan = await _context.Ilan
                .FirstOrDefaultAsync(m => m.IlanId == id);
            if (ilan == null)
            {
                return NotFound();
            }

            return View(ilan);
        }

        // POST: Ilan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Ilan == null)
            {
                return Problem("Entity set 'DbContext.Ilan'  is null.");
            }
            var ilan = await _context.Ilan.FindAsync(id);
            if (ilan != null)
            {
                _context.Ilan.Remove(ilan);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IlanExists(int id)
        {
          return (_context.Ilan?.Any(e => e.IlanId == id)).GetValueOrDefault();
        }
    }
}
