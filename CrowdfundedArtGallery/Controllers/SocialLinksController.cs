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
    public class SocialLinksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SocialLinksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SocialLinks
        public async Task<IActionResult> Index()
        {
              return _context.SocialLinks != null ? 
                          View(await _context.SocialLinks.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.SocialLinks'  is null.");
        }

        // GET: SocialLinks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.SocialLinks == null)
            {
                return NotFound();
            }

            var socialLinks = await _context.SocialLinks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (socialLinks == null)
            {
                return NotFound();
            }

            return View(socialLinks);
        }

        // GET: SocialLinks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SocialLinks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Twitter,Facebook,Instagram,TikTok")] SocialLinks socialLinks)
        {
            if (ModelState.IsValid)
            {
                _context.Add(socialLinks);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(socialLinks);
        }

        // GET: SocialLinks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.SocialLinks == null)
            {
                return NotFound();
            }

            var socialLinks = await _context.SocialLinks.FindAsync(id);
            if (socialLinks == null)
            {
                return NotFound();
            }
            return View(socialLinks);
        }

        // POST: SocialLinks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Twitter,Facebook,Instagram,TikTok")] SocialLinks socialLinks)
        {
            if (id != socialLinks.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(socialLinks);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SocialLinksExists(socialLinks.Id))
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
            return View(socialLinks);
        }

        // GET: SocialLinks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.SocialLinks == null)
            {
                return NotFound();
            }

            var socialLinks = await _context.SocialLinks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (socialLinks == null)
            {
                return NotFound();
            }

            return View(socialLinks);
        }

        // POST: SocialLinks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SocialLinks == null)
            {
                return Problem("Entity set 'ApplicationDbContext.SocialLinks'  is null.");
            }
            var socialLinks = await _context.SocialLinks.FindAsync(id);
            if (socialLinks != null)
            {
                _context.SocialLinks.Remove(socialLinks);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SocialLinksExists(int id)
        {
          return (_context.SocialLinks?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
