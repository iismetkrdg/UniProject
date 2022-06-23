using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EduProject.Models;
using EduProject.ViewModels;

namespace EduProject.Controllers
{
    public class DersNotuController : Controller
    {
        private readonly DbContext _context;
        private readonly IWebHostEnvironment hostingEnvironment;
        public DersNotuController(DbContext context,IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            this.hostingEnvironment = hostingEnvironment;
        }

        // GET: DersNotu
        public async Task<IActionResult> Index()
        {
              return _context.DersNotu != null ? 
                          View(await _context.DersNotu.ToListAsync()) :
                          Problem("Entity set 'DbContext.DersNotu'  is null.");
        }

        // GET: DersNotu/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DersNotu == null)
            {
                return NotFound();
            }

            var dersNotu = await _context.DersNotu
                .FirstOrDefaultAsync(m => m.DersNotuId == id);
            if (dersNotu == null)
            {
                return NotFound();
            }

            return View(dersNotu);
        }

        // GET: DersNotu/Create
        public IActionResult Create()
        {
            List<Ders> dersler = _context.Ders.ToList();
            ViewBag.Dersler = dersler;
            return View();
        }

        // POST: DersNotu/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DersNotuCreateViewModel dersNotu)
        {
            if (ModelState.IsValid)
            {
                string uniquefilename = null;
                if(dersNotu.FilePath!=null)
                {
                    string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath,"images");
                    uniquefilename = Guid.NewGuid().ToString() + "_" + dersNotu.FilePath.FileName;
                    string filePath = Path.Combine(uploadsFolder,uniquefilename);
                    dersNotu.FilePath.CopyTo(new FileStream(filePath,FileMode.Create));
                }
                DersNotu NewDersNotu = new DersNotu
                {
                    Konu = dersNotu.Konu,
                    DersAdı = dersNotu.DersAdı,
                    FileName = uniquefilename
                };
                System.Console.WriteLine(NewDersNotu.FileName.ToString());
                _context.Add(NewDersNotu);
                await _context.SaveChangesAsync();

                return RedirectToAction("details", new {id = NewDersNotu.DersNotuId});
            }
            return View(dersNotu);
        }

        // GET: DersNotu/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DersNotu == null)
            {
                return NotFound();
            }

            var dersNotu = await _context.DersNotu.FindAsync(id);
            if (dersNotu == null)
            {
                return NotFound();
            }
            return View(dersNotu);
        }

        // POST: DersNotu/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DersNotuId,Not")] DersNotu dersNotu)
        {
            if (id != dersNotu.DersNotuId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dersNotu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DersNotuExists(dersNotu.DersNotuId))
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
            return View(dersNotu);
        }

        // GET: DersNotu/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DersNotu == null)
            {
                return NotFound();
            }

            var dersNotu = await _context.DersNotu
                .FirstOrDefaultAsync(m => m.DersNotuId == id);
            if (dersNotu == null)
            {
                return NotFound();
            }

            return View(dersNotu);
        }

        // POST: DersNotu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DersNotu == null)
            {
                return Problem("Entity set 'DbContext.DersNotu'  is null.");
            }
            var dersNotu = await _context.DersNotu.FindAsync(id);
            if (dersNotu != null)
            {
                _context.DersNotu.Remove(dersNotu);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DersNotuExists(int id)
        {
          return (_context.DersNotu?.Any(e => e.DersNotuId == id)).GetValueOrDefault();
        }
    }
}
