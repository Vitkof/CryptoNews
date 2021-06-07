﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CryptoNews.DAL.Entities;
using CryptoNews.Core.IServices;
using CryptoNews.Core.DTO;
using CryptoNews.Models;
using CryptoNews.Models.ViewModels;

namespace CryptoNews.Controllers
{
    public class NewsController : Controller
    {
        private readonly INewsService _newsService;
        private readonly IRssSourceService _rssService;

        public NewsController(INewsService newsService, IRssSourceService rssSource)
        {
            _newsService = newsService;
            _rssService = rssSource;
        }

        // GET: News/
        public async Task<IActionResult> Index(Guid? sourceId, int pageNumber=1)
        {
            List<NewsDto> model;
            if (sourceId == null)
            {
                return NotFound();
            }

            model = (await _newsService.GetNewsBySourceId(sourceId)).ToList();
            var itemsOnPage = 20;
            var newsPerPages = model.Skip((pageNumber - 1) * itemsOnPage).Take(itemsOnPage);
            PageInfo info = new PageInfo()
            {
                PageNumber = pageNumber,
                ItemsOnPage = itemsOnPage,
                CountItems = model.Count
            };

            var result = new NewsListWithPaginator()
            {
                NewsPerPages = newsPerPages,
                PageInfo = info
            };

            return View(result);
        }

        // GET: News/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @news = _newsService.GetNewsWithRssSourceNameById(id.Value);

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
        public async Task<IActionResult> Create()
        {
            ViewData["RssSourceId"] = new SelectList(await _rssService.GetAllRssSources(), "Id", "Name");
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
            ViewData["RssSourceId"] = new SelectList(await _rssService.GetAllRssSources(), "Id", "Id", @news.RssSourceId);
            return View(@news);
        }

        // GET: News/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @news = _newsService.GetNewsById(id.Value); 
            if (@news == null)
            {
                return NotFound();
            }
            ViewData["RssSourceId"] = new SelectList(await _rssService.GetAllRssSources(), "Id", "Id", @news.RssSourceId);
            return View(@news);
        }

        // POST: News/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Title,Description,Body,PubDate,Rating,Url,RssSourceId")] NewsDto @news)
        {
            if (id != @news.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _newsService.EditNews(@news);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NewsExists(@news.Id))
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
            ViewData["RssSourceId"] = new SelectList(await _rssService.GetAllRssSources(), "Id", "Id", @news.RssSourceId);
            return View(@news);
        }

        // GET: News/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @news =  _newsService.GetNewsById(id.Value);

            if (@news == null)
            {
                return NotFound();
            }

            return View(@news);
        }

        // POST: News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var @news = _newsService.GetNewsById(id);//_context.News.FindAsync(id);
            await _newsService.DeleteNews(@news);
            return RedirectToAction(nameof(Index));
        }

        private bool NewsExists(Guid id)
        {
            return _newsService.Exist(id);
        }
    }
}
