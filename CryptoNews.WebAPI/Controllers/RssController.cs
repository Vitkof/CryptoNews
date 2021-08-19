using CryptoNews.Core.DTO;
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
    public class RssController : ControllerBase
    {
        private readonly IRssSourceService _rssService;

        public RssController(IRssSourceService rssSvc)
        {
            _rssService = rssSvc;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var @src = _rssService.GetRssSourceById(id);

            return Ok(@src);
        }

        [HttpGet]
        public async Task<IActionResult> Get(string name, string url)
        {
            var @srces = await _rssService.GetAllRssSources();
            if (!string.IsNullOrEmpty(name))
            {
                @srces = srces.Where(dto => dto.Name.Contains(name));
            }
            if (!string.IsNullOrEmpty(url))
            {
                @srces = srces.Where(dto => dto.Name.Contains(url));
            }
            return Ok(@srces);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RssSourceDto request)
        {
            await _rssService.AddRssSource(request);
            
            return CreatedAtAction("Create", request);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] string value)
        {
            return Ok();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch()
        {
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok();
        }

    }
}
