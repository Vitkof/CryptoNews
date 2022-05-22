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
    public class RssController : ControllerBase
    {
        private readonly IRssSourceService _rssService;

        public RssController(IRssSourceService rssSvc)
        {
            _rssService = rssSvc;
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var @src = _rssService.GetRssSourceById(id);
            if (@src == null)
            {
                return NotFound();
            }

            return Ok(@src);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var getAll = await _rssService.GetAllRssSources();

                return Ok(getAll);
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                return NotFound(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RssSourceDto rssDto)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                await _rssService.AddRssSource(rssDto);
                return CreatedAtAction("Post", rssDto);
            }
            catch(Exception ex)
            {
                Log.Error($"POST error adding RSS: {ex}");
            }

            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] RssSourceDto value)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var rss = _rssService.GetRssSourceById(id);
                if (rss == null) return NotFound();
                await _rssService.EditRssSource(rss);
                return Ok();
            }
            catch(Exception)
            {

            }
            return BadRequest("Error updating RSS");
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(Guid id, [FromBody] JsonPatchDocument<RssSourceDto> patchDoc)
        {
            try
            {
                if (patchDoc != null)
                {
                    if (!ModelState.IsValid)
                    {
                        return BadRequest(ModelState);
                    }

                    var rss = _rssService.GetRssSourceById(id);
                    if (rss == null)
                    {
                        return NotFound();
                    }

                    patchDoc.ApplyTo(rss);
                    await _rssService.EditRssSource(rss);
                    return new ObjectResult(rss);
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch(Exception ex)
            {
                Log.Error($"PATCH error updating RSS: {ex}");
            }
            return BadRequest("Error updating RSS");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var rss = _rssService.GetRssSourceById(id);
            await _rssService.DeleteRssSource(rss);
            return Ok();
        }
    }
}
