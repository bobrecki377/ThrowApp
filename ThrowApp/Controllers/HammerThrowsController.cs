using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ThrowApp.Data;
using ThrowApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace ThrowApp.Controllers
{
    public class HammerThrowsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HammerThrowsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: HammerThrows
        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View(await _context.HammerThrow.ToListAsync());
        }

        // GET: HammerThrows/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hammerThrow = await _context.HammerThrow
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hammerThrow == null)
            {
                return NotFound();
            }

            return View(hammerThrow);
        }

        // GET: HammerThrows/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        public IActionResult Search()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> SearchHammerResult(string Name, string Country, string Result, string Position)
        {

            var query = _context.HammerThrow.AsQueryable();


            if (!string.IsNullOrEmpty(Name))
            {
                query = query.Where(j => j.Name.Contains(Name));
            }

            if (!string.IsNullOrEmpty(Country))
            {
                query = query.Where(j => j.Country == Country);
            }

            if (!string.IsNullOrEmpty(Result))
            {
                query = query.Where(j => j.Result.ToString().Contains(Result));
            }

            if (!string.IsNullOrEmpty(Position))
            {
                query = query.Where(j => j.Position.ToString().Contains(Position));
            }

            var results = query.ToListAsync();

            return View("Index", await results);

        }

        // POST: HammerThrows/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Country,Result,Position")] HammerThrow hammerThrow)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hammerThrow);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(hammerThrow);
        }

        // GET: HammerThrows/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hammerThrow = await _context.HammerThrow.FindAsync(id);
            if (hammerThrow == null)
            {
                return NotFound();
            }
            return View(hammerThrow);
        }

        // POST: HammerThrows/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Country,Result,Position")] HammerThrow hammerThrow)
        {
            if (id != hammerThrow.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hammerThrow);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HammerThrowExists(hammerThrow.Id))
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
            return View(hammerThrow);
        }

        // GET: HammerThrows/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hammerThrow = await _context.HammerThrow
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hammerThrow == null)
            {
                return NotFound();
            }

            return View(hammerThrow);
        }

        // POST: HammerThrows/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hammerThrow = await _context.HammerThrow.FindAsync(id);
            if (hammerThrow != null)
            {
                _context.HammerThrow.Remove(hammerThrow);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HammerThrowExists(int id)
        {
            return _context.HammerThrow.Any(e => e.Id == id);
        }
    }
}
