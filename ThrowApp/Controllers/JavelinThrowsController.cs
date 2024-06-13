using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.EntityFrameworkCore;
using ThrowApp.Data;
using ThrowApp.Models;

namespace ThrowApp.Controllers
{
    public class JavelinThrowsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JavelinThrowsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: JavelinThrows
        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View(await _context.JavelinThrow.ToListAsync());
        }

        [Authorize]
        public IActionResult Search()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> SearchJavelinResult(string Name,string Country,string Result,string Position)
        {

            var query = _context.JavelinThrow.AsQueryable();


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

            return View("Index",await results);

        }

        // GET: JavelinThrows/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var javelinThrow = await _context.JavelinThrow
                .FirstOrDefaultAsync(m => m.Id == id);
            if (javelinThrow == null)
            {
                return NotFound();
            }

            return View(javelinThrow);
        }

        // GET: JavelinThrows/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: JavelinThrows/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Country,Result,Position")] JavelinThrow javelinThrow)
        {
            if (ModelState.IsValid)
            {
                _context.Add(javelinThrow);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(javelinThrow);
        }

        // GET: JavelinThrows/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var javelinThrow = await _context.JavelinThrow.FindAsync(id);
            if (javelinThrow == null)
            {
                return NotFound();
            }
            return View(javelinThrow);
        }

        // POST: JavelinThrows/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Country,Result,Position")] JavelinThrow javelinThrow)
        {
            if (id != javelinThrow.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(javelinThrow);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JavelinThrowExists(javelinThrow.Id))
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
            return View(javelinThrow);
        }

        // GET: JavelinThrows/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var javelinThrow = await _context.JavelinThrow
                .FirstOrDefaultAsync(m => m.Id == id);
            if (javelinThrow == null)
            {
                return NotFound();
            }

            return View(javelinThrow);
        }

        // POST: JavelinThrows/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var javelinThrow = await _context.JavelinThrow.FindAsync(id);
            if (javelinThrow != null)
            {
                _context.JavelinThrow.Remove(javelinThrow);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JavelinThrowExists(int id)
        {
            return _context.JavelinThrow.Any(e => e.Id == id);
        }
    }
}
