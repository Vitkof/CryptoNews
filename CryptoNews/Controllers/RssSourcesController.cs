﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CryptoNews.DAL.Entities;

namespace CryptoNews.Controllers
{
    public class RssSourcesController : Controller
    {
        private readonly CryptoNewsContext _context;

        public RssSourcesController(CryptoNewsContext context)
        {
            _context = context;
        }

        // GET: RssSources
        public async Task<IActionResult> Index()
        {
            return View(await _context.RssSources.ToListAsync());
        }

        // GET: RssSources/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rssSource = await _context.RssSources
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rssSource == null)
            {
                return NotFound();
            }

            return View(rssSource);
        }

        // GET: RssSources/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RssSources/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Url")] RssSource rssSource)
        {
            if (ModelState.IsValid)
            {
                rssSource.Id = Guid.NewGuid();
                _context.RssSources.Add(rssSource);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rssSource);
        }

        // GET: RssSources/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rssSource = await _context.RssSources.FindAsync(id);
            if (rssSource == null)
            {
                return NotFound();
            }
            return View(rssSource);
        }

        // POST: RssSources/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Url")] RssSource rssSource)
        {
            if (id != rssSource.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.RssSources.Update(rssSource);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RssSourceExists(rssSource.Id))
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
            return View(rssSource);
        }

        // GET: RssSources/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rssSource = await _context.RssSources
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rssSource == null)
            {
                return NotFound();
            }

            return View(rssSource);
        }

        // POST: RssSources/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var rssSource = await _context.RssSources.FindAsync(id);
            _context.RssSources.Remove(rssSource);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RssSourceExists(Guid id)
        {
            return _context.RssSources.Any(e => e.Id == id);
        }
    }
}
