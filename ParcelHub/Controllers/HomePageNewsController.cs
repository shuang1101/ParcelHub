using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ParcelHub.DatabaseConnection;
using ParcelHub.Models;

namespace ParcelHub.Controllers
{
    public class HomePageNewsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomePageNewsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: HomePageNews
        public async Task<IActionResult> Index()
        {
            return View(await _context.HomePageNews.ToListAsync());
        }

        // GET: HomePageNews/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var homePageNews = await _context.HomePageNews
                .FirstOrDefaultAsync(m => m.Id == id);
            if (homePageNews == null)
            {
                return NotFound();
            }

            return View(homePageNews);
        }

        // GET: HomePageNews/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: HomePageNews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Content,ImageUrl,Title,Post,Id")] HomePageNews homePageNews)
        {
            if (ModelState.IsValid)
            {
                _context.Add(homePageNews);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(homePageNews);
        }

        // GET: HomePageNews/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var homePageNews = await _context.HomePageNews.FindAsync(id);
            if (homePageNews == null)
            {
                return NotFound();
            }
            return View(homePageNews);
        }

        // POST: HomePageNews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Content,ImageUrl,Title,Post,Id")] HomePageNews homePageNews)
        {
            if (id != homePageNews.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(homePageNews);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HomePageNewsExists(homePageNews.Id))
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
            return View(homePageNews);
        }

        // GET: HomePageNews/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var homePageNews = await _context.HomePageNews
                .FirstOrDefaultAsync(m => m.Id == id);
            if (homePageNews == null)
            {
                return NotFound();
            }

            return View(homePageNews);
        }

        // POST: HomePageNews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var homePageNews = await _context.HomePageNews.FindAsync(id);
            _context.HomePageNews.Remove(homePageNews);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HomePageNewsExists(int id)
        {
            return _context.HomePageNews.Any(e => e.Id == id);
        }
    }
}
