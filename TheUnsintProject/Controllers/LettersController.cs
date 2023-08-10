using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TheUnsintProject.Contracts;
using TheUnsintProject.Data;
using TheUnsintProject.Models;

namespace TheUnsintProject.Controllers
{
    public class LettersController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public LettersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Letters
        public async Task<IActionResult> Index(string? q,
            string? filter)
        {
            return  _unitOfWork.LetterRepository != null ?
                        View(await _unitOfWork.LetterRepository.Get()) :
                        Problem("Entity set 'TUPDbContext.Letter'  is null.");
        }

        // GET: Letters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _unitOfWork.LetterRepository == null)
            {
                return NotFound();
            }

            var letter = await _unitOfWork.LetterRepository
               .GetById(id.Value);

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
                _unitOfWork.LetterRepository.Insert(letter);
                await _unitOfWork.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(letter);
        }

        private async Task<bool> LetterExists(int id)
        {
            var letter = await _unitOfWork.LetterRepository.GetById(id);
            return letter != null;
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
