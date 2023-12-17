using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CrowdfundedArtGallery.Data;
using CrowdfundedArtGallery.Models;

namespace CrowdfundedArtGallery.Controllers
{
    public class ArtPostsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ArtPostsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ArtPosts
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ArtPosts.Include(a => a.Category).Include(a => a.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ArtPosts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ArtPosts == null)
            {
                return NotFound();
            }

            var artPost = await _context.ArtPosts
                .Include(a => a.Category)
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (artPost == null)
            {
                return NotFound();
            }

            return View(artPost);
        }

        // GET: ArtPosts/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "ID", "ID");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: ArtPosts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,Title,Description,Address,PhoneNumber,Price,CategoryId,ImageFolder")] ArtPost artPost)
        {
            if (ModelState.IsValid)
            {
                _context.Add(artPost);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "ID", "ID", artPost.CategoryId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", artPost.UserId);
            return View(artPost);
        }

        // GET: ArtPosts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ArtPosts == null)
            {
                return NotFound();
            }

            var artPost = await _context.ArtPosts.FindAsync(id);
            if (artPost == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "ID", "ID", artPost.CategoryId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", artPost.UserId);
            return View(artPost);
        }

        // POST: ArtPosts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserId,Title,Description,Address,PhoneNumber,Price,CategoryId,ImageFolder")] ArtPost artPost)
        {
            if (id != artPost.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(artPost);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArtPostExists(artPost.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "ID", "ID", artPost.CategoryId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", artPost.UserId);
            return View(artPost);
        }

        // GET: ArtPosts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ArtPosts == null)
            {
                return NotFound();
            }

            var artPost = await _context.ArtPosts
                .Include(a => a.Category)
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (artPost == null)
            {
                return NotFound();
            }

            return View(artPost);
        }

        // POST: ArtPosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ArtPosts == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ArtPosts'  is null.");
            }
            var artPost = await _context.ArtPosts.FindAsync(id);
            if (artPost != null)
            {
                _context.ArtPosts.Remove(artPost);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArtPostExists(int id)
        {
          return (_context.ArtPosts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
