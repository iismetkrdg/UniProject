using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EduProject.Models;
using Microsoft.AspNetCore.Authorization;

namespace EduProject.Controllers
{
    public class ForumCommentController : Controller
    {
        private readonly DbContext _context;

        public ForumCommentController(DbContext context)
        {
            _context = context;
        }

        // GET: ForumComment
        public async Task<IActionResult> Index()
        {
              return _context.ForumComment != null ? 
                          View(await _context.ForumComment.ToListAsync()) :
                          Problem("Entity set 'DbContext.ForumComment'  is null.");
        }

        // GET: ForumComment/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ForumComment == null)
            {
                return NotFound();
            }

            var forumComment = await _context.ForumComment
                .FirstOrDefaultAsync(m => m.ForumCommentId == id);
            if (forumComment == null)
            {
                return NotFound();
            }

            return View(forumComment);
        }

        // GET: ForumComment/Create
        
        public IActionResult Create(int id)
        {
            ViewBag.Id = id;

            return View();
        }

        // POST: ForumComment/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ForumCommentId,Message,ForumId")] ForumComment forumComment)
        {
            if (ModelState.IsValid)
            {

                _context.Add(forumComment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(forumComment);
        }

        // GET: ForumComment/Edit/5
        [Authorize(Roles ="admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ForumComment == null)
            {
                return NotFound();
            }

            var forumComment = await _context.ForumComment.FindAsync(id);
            if (forumComment == null)
            {
                return NotFound();
            }
            return View(forumComment);
        }

        // POST: ForumComment/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles ="admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ForumCommentId,Message")] ForumComment forumComment)
        {
            if (id != forumComment.ForumCommentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(forumComment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ForumCommentExists(forumComment.ForumCommentId))
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
            return View(forumComment);
        }
        [Authorize(Roles ="admin")]
        // GET: ForumComment/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ForumComment == null)
            {
                return NotFound();
            }

            var forumComment = await _context.ForumComment
                .FirstOrDefaultAsync(m => m.ForumCommentId == id);
            if (forumComment == null)
            {
                return NotFound();
            }

            return View(forumComment);
        }
        [Authorize(Roles ="admin")]
        // POST: ForumComment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ForumComment == null)
            {
                return Problem("Entity set 'DbContext.ForumComment'  is null.");
            }
            var forumComment = await _context.ForumComment.FindAsync(id);
            if (forumComment != null)
            {
                _context.ForumComment.Remove(forumComment);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ForumCommentExists(int id)
        {
          return (_context.ForumComment?.Any(e => e.ForumCommentId == id)).GetValueOrDefault();
        }
    }
}
