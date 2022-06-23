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
    public class ForumBaslikController : Controller
    {
        private readonly DbContext _context;

        public ForumBaslikController(DbContext context)
        {
            _context = context;
        }

        // GET: ForumBaslik
        public async Task<IActionResult> Index()
        {
              return _context.ForumBaslik != null ? 
                          View(await _context.ForumBaslik.ToListAsync()) :
                          Problem("Entity set 'DbContext.ForumBaslik'  is null.");
        }

        // GET: ForumBaslik/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ForumBaslik == null)
            {
                return NotFound();
            }

            var forumBaslik = await _context.ForumBaslik
                .FirstOrDefaultAsync(m => m.ForumBaslikId == id);
            if (forumBaslik == null)
            {
                return NotFound();
            }
            List<ForumComment> yazilar = _context.ForumComment.Where(x=>x.ForumId==id).ToList();
            ViewBag.Yazilar=yazilar;
            ViewBag.Id = id;
            return View(forumBaslik);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Details([Bind("ForumCommentId,Message,ForumId,Creator")] ForumComment forumComment)
        {

            if (ModelState.IsValid)
            {
                _context.Add(forumComment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details));
            }
            return View(forumComment);
        }
        // GET: ForumBaslik/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ForumBaslik/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ForumBaslikId,Name,Creator")] ForumBaslik forumBaslik)
        {
            if (ModelState.IsValid)
            {
                _context.Add(forumBaslik);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(forumBaslik);
        }

        // GET: ForumBaslik/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ForumBaslik == null)
            {
                return NotFound();
            }

            var forumBaslik = await _context.ForumBaslik.FindAsync(id);
            if (forumBaslik == null)
            {
                return NotFound();
            }
            return View(forumBaslik);
        }

        // POST: ForumBaslik/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ForumBaslikId,Name,Creator")] ForumBaslik forumBaslik)
        {
            if (id != forumBaslik.ForumBaslikId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(forumBaslik);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ForumBaslikExists(forumBaslik.ForumBaslikId))
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
            return View(forumBaslik);
        }

        // GET: ForumBaslik/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ForumBaslik == null)
            {
                return NotFound();
            }

            var forumBaslik = await _context.ForumBaslik
                .FirstOrDefaultAsync(m => m.ForumBaslikId == id);
            if (forumBaslik == null)
            {
                return NotFound();
            }

            return View(forumBaslik);
        }

        // POST: ForumBaslik/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ForumBaslik == null)
            {
                return Problem("Entity set 'DbContext.ForumBaslik'  is null.");
            }
            var forumBaslik = await _context.ForumBaslik.FindAsync(id);
            if (forumBaslik != null)
            {
                _context.ForumBaslik.Remove(forumBaslik);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ForumBaslikExists(int id)
        {
          return (_context.ForumBaslik?.Any(e => e.ForumBaslikId == id)).GetValueOrDefault();
        }
    }
}
