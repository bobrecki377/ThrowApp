using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ThrowApp.Data;
using ThrowApp.Models;

namespace ThrowApp.Controllers
{
    public class DiscusThrowsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DiscusThrowsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: DiscusThrows
        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View(await _context.DiscusThrow.ToListAsync());
        }

        // GET: DiscusThrows/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discusThrow = await _context.DiscusThrow
                .FirstOrDefaultAsync(m => m.Id == id);
            if (discusThrow == null)
            {
                return NotFound();
            }

            return View(discusThrow);
        }

        // GET: DiscusThrows/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // GET: DiscusThrows/Search
        [Authorize]
        public IActionResult Search()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> SearchDiscusResult(string Name, string Country, string Result, string Position)
        {

            var query = _context.DiscusThrow.AsQueryable();


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

        // POST: DiscusThrows/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Country,Result,Position")] DiscusThrow discusThrow)
        {
            if (ModelState.IsValid)
            {
                _context.Add(discusThrow);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(discusThrow);
        }

        // GET: DiscusThrows/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discusThrow = await _context.DiscusThrow.FindAsync(id);
            if (discusThrow == null)
            {
                return NotFound();
            }
            return View(discusThrow);
        }

        // POST: DiscusThrows/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Country,Result,Position")] DiscusThrow discusThrow)
        {
            if (id != discusThrow.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(discusThrow);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiscusThrowExists(discusThrow.Id))
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
            return View(discusThrow);
        }

        // GET: DiscusThrows/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var discusThrow = await _context.DiscusThrow
                .FirstOrDefaultAsync(m => m.Id == id);
            if (discusThrow == null)
            {
                return NotFound();
            }

            return View(discusThrow);
        }

        // POST: DiscusThrows/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var discusThrow = await _context.DiscusThrow.FindAsync(id);
            if (discusThrow != null)
            {
                _context.DiscusThrow.Remove(discusThrow);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DiscusThrowExists(int id)
        {
            return _context.DiscusThrow.Any(e => e.Id == id);
        }
    }
}
