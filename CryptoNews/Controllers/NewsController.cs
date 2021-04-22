using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CryptoNews.DAL.Entities;
using CryptoNews.Models.ViewModels;
using CryptoNews.Core.IServices;
using CryptoNews.Core.DTO;

namespace CryptoNews.Controllers
{
    public class NewsController : Controller
    {
        private readonly INewService _newService;

        public NewsController(INewService newService)
        {
            _newService = newService;
        }

        // GET: News
        public async Task<IActionResult> Index(Guid? sourceId)
        {
            List<NewDto> model;
            if (sourceId == null)
            {
                return NotFound();
            }

            model = (await _newService.GetNewBySourceId(sourceId)).ToList();
            
            
            return View(model);
        }

        // GET: News/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @new = await _newService.GetNewWithRssSourceNameById(id.Value);

            if (@new == null)
            {
                return NotFound();
            }
            var viewModel = new NewWithRssSourceNameDto()
            {
                Id = @new.Id,
                Title = @new.Title,
                Description = @new.Description,
                Body = @new.Body,
                PubDate = @new.PubDate,
                Rating = @new.Rating,
                Url = @new.Url,
                RssSourceId = @new.RssSourceId,
                RssSourceName = @new.RssSourceName

            };
            return View(@new);
        }

        // GET: News/Create
        public IActionResult Create()
        {
            ViewData["RssSourceId"] = new SelectList(_context.RssSources, "Id", "Id");
            return View();
        }

        // POST: News/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,Body,PubDate,Rating,Url,RssSourceId")] New @new)
        {
            if (ModelState.IsValid)
            {
                @new.Id = Guid.NewGuid();
                _context.Add(@new);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RssSourceId"] = new SelectList(_context.RssSources, "Id", "Id", @new.RssSourceId);
            return View(@new);
        }

        // GET: News/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @new = await _context.News.FindAsync(id);
            if (@new == null)
            {
                return NotFound();
            }
            ViewData["RssSourceId"] = new SelectList(_context.RssSources, "Id", "Id", @new.RssSourceId);
            return View(@new);
        }

        // POST: News/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Title,Description,Body,PubDate,Rating,Url,RssSourceId")] New @new)
        {
            if (id != @new.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@new);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NewExists(@new.Id))
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
            ViewData["RssSourceId"] = new SelectList(_context.RssSources, "Id", "Id", @new.RssSourceId);
            return View(@new);
        }

        // GET: News/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @new = await _context.News
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@new == null)
            {
                return NotFound();
            }

            return View(@new);
        }

        // POST: News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var @new = await _context.News.FindAsync(id);
            _context.News.Remove(@new);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NewExists(Guid id)
        {
            return _context.News.Any(e => e.Id == id);
        }
    }
}
