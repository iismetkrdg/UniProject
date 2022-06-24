using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EduProject.Models;
using EduProject.ViewModels;
using Microsoft.AspNetCore.Hosting;
using X.PagedList;

namespace EduProject.Controllers
{
    public class SınavController : Controller
    {
        private readonly DbContext _context;
        private readonly IWebHostEnvironment hostingEnvironment;

        public SınavController(DbContext context,
                                IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            this.hostingEnvironment = hostingEnvironment;
        }

        // GET: Sınav
        public IActionResult Index(int? page)
        {
            var pageSize = 20;
            var pageNumber = page ?? 1;
            return View(_context.Sınav.ToPagedList(pageNumber, pageSize));
        }

        // GET: Sınav/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Sınav == null)
            {
                return NotFound();
            }

            var sınav = await _context.Sınav
                .FirstOrDefaultAsync(m => m.SınavId == id);
            if (sınav == null)
            {
                return NotFound();
            }

            return View(sınav);
        }

        // GET: Sınav/Create
        public IActionResult Create()
        {
            List<Ders> dersler = _context.Ders.ToList();
            ViewBag.Dersler = dersler;
            return View();
        }

        // POST: Sınav/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SınavCreateViewModel sınav)
        {
            if (ModelState.IsValid)
            {
                string uniquefilename = null;
                if(sınav.FilePath!=null)
                {
                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath,"images");
                    uniquefilename = Guid.NewGuid().ToString() + "_" + sınav.FilePath.FileName;
                    string filePath = Path.Combine(uploadsFolder,uniquefilename);
                    sınav.FilePath.CopyTo(new FileStream(filePath,FileMode.Create));
                }
                Sınav NewSınav = new Sınav
                {
                    Name = sınav.Name,
                    DersAdı = sınav.DersAdı,
                    FileName = uniquefilename
                };
                _context.Add(NewSınav);
                await _context.SaveChangesAsync();

                return RedirectToAction("index");
            }
            return View(sınav);
        }

        // GET: Sınav/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Sınav == null)
            {
                return NotFound();
            }

            var sınav = await _context.Sınav.FindAsync(id);
            if (sınav == null)
            {
                return NotFound();
            }
            List<Ders> dersler = _context.Ders.ToList();
            ViewBag.Dersler = dersler;
            return View(sınav);
        }

        // POST: Sınav/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SınavId,Name,DersAdı")] Sınav sınav)
        {
            if (id != sınav.SınavId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sınav);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SınavExists(sınav.SınavId))
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
            return View(sınav);
        }

        // GET: Sınav/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Sınav == null)
            {
                return NotFound();
            }

            var sınav = await _context.Sınav
                .FirstOrDefaultAsync(m => m.SınavId == id);
            if (sınav == null)
            {
                return NotFound();
            }

            return View(sınav);
        }

        // POST: Sınav/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Sınav == null)
            {
                return Problem("Entity set 'DbContext.Sınav'  is null.");
            }
            var sınav = await _context.Sınav.FindAsync(id);
            if (sınav != null)
            {
                _context.Sınav.Remove(sınav);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SınavExists(int id)
        {
          return (_context.Sınav?.Any(e => e.SınavId == id)).GetValueOrDefault();
        }
    }
}
