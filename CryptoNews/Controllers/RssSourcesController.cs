using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CryptoNews.DAL.Entities;
using CryptoNews.Core.DTO;
using CryptoNews.Models.ViewModels;
using CryptoNews.Models;
using CryptoNews.Core.IServices;

namespace CryptoNews.Controllers
{
    public class RssSourcesController : Controller
    {
        private readonly INewsService _newsService;
        private readonly IRssSourceService _rssService;

        public RssSourcesController(INewsService newsSvc,
                                    IRssSourceService rssSvc)
        {
            _newsService = newsSvc;
            _rssService = rssSvc;
        }

        // GET: RssSources
        public Task<IActionResult> Index() =>
            IndexInternal();
        private async Task<IActionResult> IndexInternal()
        {
            var allRss = await _rssService.GetAllRssSources();
            return View(allRss);
        }

        // GET: RssSources/Details/5
        public IActionResult Details(Guid? id) =>
            DetailsInternal(id);
        private IActionResult DetailsInternal(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var rssSource = _rssService.GetRssSourceById(id.Value);
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Url")] RssSourceDto rssSource)
        {
            if (ModelState.IsValid)
            {
                rssSource.Id = Guid.NewGuid();
                await _rssService.AddRssSource(rssSource);
                return RedirectToAction(nameof(Index));
            }
            return View(rssSource);
        }

        // GET: RssSources/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rssSource = _rssService.GetRssSourceById(id.Value);
            if (rssSource == null)
            {
                return NotFound();
            }
            return View(rssSource);
        }

        // POST: RssSources/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Name,Url")] RssSourceDto rssSource)
        {
            if (id != rssSource.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _rssService.EditRssSource(rssSource);
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
        public IActionResult Delete(Guid? id) =>
            DeleteInternal(id);
        private IActionResult DeleteInternal(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rssSource = _rssService.GetRssSourceById(id.Value);
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
            var rssSource = _rssService.GetRssSourceById(id);
            await _rssService.DeleteRssSource(rssSource);
            return RedirectToAction(nameof(Index));
        }

        private bool RssSourceExists(Guid id)
        {
            return _rssService.Exist(id);
        }

        // GET: RssSources/News/
        public async Task<IActionResult> News(Guid sourceId, int pageNumber = 1)
        {
            List<NewsDto> model;

            model = (await _newsService.GetNewsBySourceId(sourceId)).ToList();


            var itemsOnPage = 12;
            var newsPerPages = model.Skip((pageNumber - 1) * itemsOnPage).Take(itemsOnPage);
            PageInfo info = new()
            {
                PageNumber = pageNumber,
                ItemsOnPage = itemsOnPage,
                CountItems = model.Count
            };

            var res = new NewsListWithPaginator()
            {
                NewsPerPages = newsPerPages,
                PageInfo = info,
                IsAdmin = false
            };

            return View(res);
        }
    }
}
