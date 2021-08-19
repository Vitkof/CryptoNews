using CryptoNews.Core.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoNews.WebAPI.Controllers
{   
    [ApiController]
    [Route("api/[controller]")]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;

        public NewsController(INewsService newsSvc)
        {
            _newsService = newsSvc;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var @news = _newsService.GetNewsById(id);

            //if (@news == null)
            //{
            //    return notfound();
            //}

            return Ok(@news);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var @newsColl = await _newsService.GetNewsBySourceId(null);
            return Ok(@newsColl);
        }

    }
}
