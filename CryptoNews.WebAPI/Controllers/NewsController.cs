using CryptoNews.Core.DTO;
using CryptoNews.Core.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoNews.WebAPI.Controllers
{   
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;

        public NewsController(INewsService newsSvc)
        {
            _newsService = newsSvc;
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                var @news = _newsService.GetNewsById(id);
                if (@news == null)
                {
                    return NotFound();
                }

                return Ok(@news);
            }
            catch(Exception ex)
            {
                return BadRequest($"Error {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var @newsColl = await _newsService.GetNewsBySourceId(null);
            return Ok(@newsColl);
        }

        [HttpPut]
        public async Task<IActionResult> Put(NewsDto dto)
        {
            try
            {
                var res = await _newsService.EditNews(dto);
                if (res == 0)
                    return BadRequest("News doesn't updated");

                return Ok("News succeeded updated");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error {ex.Message}");
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(Guid id, [FromBody] JsonPatchDocument<NewsDto> patchDoc)
        {
            try
            {
                if (patchDoc != null)
                {
                    if (!ModelState.IsValid)
                    {
                        return BadRequest(ModelState);
                    }

                    var news = _newsService.GetNewsById(id);
                    if (news == null)
                    {
                        return NotFound();
                    }

                    patchDoc.ApplyTo(news);
                    await _newsService.EditNews(news);
                    return new ObjectResult(news);
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                Log.Error($"PATCH error updating News: {ex}");
            }
            return BadRequest("Error updating News");
        }
    }
}
