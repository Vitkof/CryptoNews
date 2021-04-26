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
        private readonly INewsService _newsService;

        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }

        // GET: News
        public async Task<IActionResult> Index(Guid? sourceId)
        {
            List<NewsDto> model;
            if (sourceId == null)
            {
                return NotFound();
            }

            model = (await _newsService.GetNewsBySourceId(sourceId)).ToList();
            
            
            return View(model);
        }

        // GET: News/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @news = await _newsService.GetNewsWithRssSourceNameById(id.Value);

            if (@news == null)
            {
                return NotFound();
            }
            var viewModel = new NewsWithRssSourceNameDto()
            {
                Id = @news.Id,
                Title = @news.Title,
                Description = @news.Description,
                Body = @news.Body,
                PubDate = @news.PubDate,
                Rating = @news.Rating,
                Url = @news.Url,
                RssSourceId = @news.RssSourceId,
                RssSourceName = @news.RssSourceName

            };
            return View(@news);
        }

        // GET: News/Create
        public IActionResult Create()
        {
            ViewData["RssSourceId"] = new SelectList(_newsService.GetAll, "Id", "Id");
            return View();
        }

        // POST: News/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,Body,PubDate,Rating,Url,RssSourceId")] NewsDto @news)
        {
            if (ModelState.IsValid)
            {
                @news.Id = Guid.NewGuid();
                await _newsService.AddNews(@news);
                return RedirectToAction(nameof(Index));
            }
            ViewData["RssSourceId"] = new SelectList(_context.RssSources, "Id", "Id", @news.RssSourceId);
            return View(@news);
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
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Title,Description,Body,PubDate,Rating,Url,RssSourceId")] News @new)
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
