using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TheUnsintProject.Data;
using TheUnsintProject.Models;

namespace TheUnsintProject.Controllers
{
    public class LettersController : Controller
    {
        private readonly TUPDbContext _context;

        public LettersController(TUPDbContext context)
        {
            _context = context;
        }

        // GET: Letters
        public async Task<IActionResult> Index()
        {
              return _context.Letter != null ? 
                          View(await _context.Letter.ToListAsync()) :
                          Problem("Entity set 'TUPDbContext.Letter'  is null.");
        }

        // GET: Letters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Letter == null)
            {
                return NotFound();
            }

            var letter = await _context.Letter
                .FirstOrDefaultAsync(m => m.Id == id);

            if (letter == null)
            {
                return NotFound();
            }

            return View(letter);
        }

        // GET: Letters/Create
        public IActionResult Create()
        {
            PopulateColors();
            return View();
        }

        // POST: Letters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Message,Color")] Letter letter)
        {
            if (ModelState.IsValid)
            {
                _context.Add(letter);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(letter);
        }

        // GET: Letters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Letter == null)
            {
                return NotFound();
            }

            var letter = await _context.Letter.FindAsync(id);
            if (letter == null)
            {
                return NotFound();
            }
            return View(letter);
        }

        // POST: Letters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Message,Color")] Letter letter)
        {
            if (id != letter.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(letter);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LetterExists(letter.Id))
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
            return View(letter);
        }

        // GET: Letters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Letter == null)
            {
                return NotFound();
            }

            var letter = await _context.Letter
                .FirstOrDefaultAsync(m => m.Id == id);
            if (letter == null)
            {
                return NotFound();
            }

            return View(letter);
        }

        // POST: Letters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Letter == null)
            {
                return Problem("Entity set 'TUPDbContext.Letter'  is null.");
            }
            var letter = await _context.Letter.FindAsync(id);
            if (letter != null)
            {
                _context.Letter.Remove(letter);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LetterExists(int id)
        {
          return (_context.Letter?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private void PopulateColors(object selected = null)
        {
            var colors = new List<SelectListItem>();

            foreach (LetterColor color in Enum.GetValues(typeof(LetterColor)))
            {
                colors.Add(new SelectListItem
                {
                    Text = color.ToString(),
                    Value = color.ToString(),
                    Selected = (color.ToString() == selected?.ToString())
                });
            }

            ViewBag.Colors = new SelectList(colors, "Value", "Text", selected);
        }
    }
}
