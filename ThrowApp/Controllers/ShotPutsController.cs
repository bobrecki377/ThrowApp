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
    public class ShotPutsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ShotPutsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ShotPuts
        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View(await _context.ShotPut.ToListAsync());
        }

        [Authorize]
        public IActionResult Search()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> SearchShotPutResult(string Name, string Country, string Result, string Position)
        {

            var query = _context.ShotPut.AsQueryable();


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

        // GET: ShotPuts/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shotPut = await _context.ShotPut
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shotPut == null)
            {
                return NotFound();
            }

            return View(shotPut);
        }

        // GET: ShotPuts/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: ShotPuts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Country,Result,Position")] ShotPut shotPut)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shotPut);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(shotPut);
        }

        // GET: ShotPuts/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shotPut = await _context.ShotPut.FindAsync(id);
            if (shotPut == null)
            {
                return NotFound();
            }
            return View(shotPut);
        }

        // POST: ShotPuts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Country,Result,Position")] ShotPut shotPut)
        {
            if (id != shotPut.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shotPut);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShotPutExists(shotPut.Id))
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
            return View(shotPut);
        }

        // GET: ShotPuts/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shotPut = await _context.ShotPut
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shotPut == null)
            {
                return NotFound();
            }

            return View(shotPut);
        }

        // POST: ShotPuts/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shotPut = await _context.ShotPut.FindAsync(id);
            if (shotPut != null)
            {
                _context.ShotPut.Remove(shotPut);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShotPutExists(int id)
        {
            return _context.ShotPut.Any(e => e.Id == id);
        }
    }
}
