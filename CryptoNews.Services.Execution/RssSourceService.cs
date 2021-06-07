using CryptoNews.Core.DTO;
using CryptoNews.Core.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CryptoNews.DAL.Repositories;
using CryptoNews.DAL.Entities;
using CryptoNews.DAL.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace CryptoNews.Services.Implement
{
    public class RssSourceService : IRssSourceService
    {
        private readonly IUnitOfWork _unit;

        public RssSourceService(IUnitOfWork unitOfWork)
        {
            _unit = unitOfWork;
        }

        

        public async Task AddRssSource(RssSourceDto rd)
        {
            var rss = new RssSource()
            {
                Id = rd.Id,
                Name = rd.Name,
                Url = rd.Url
            };
            await _unit.RssSources.Create(rss);
        }

        public async Task AddRangeRssSources(IEnumerable<RssSourceDto> rssDto)
        {
            var range = rssDto.Select(rd => new RssSource()
            {
                Id = rd.Id,
                Name = rd.Name,
                Url = rd.Url
            }).ToList();
            await _unit.RssSources.CreateRange(range);
        }

        public async Task<int> DeleteRssSource(RssSourceDto rd)
        {
            return await Task.Run(async () =>
            {
                await _unit.RssSources.Delete(rd.Id);
                return await _unit.SaveChangesAsync();
            });
        }

        public async Task<int> EditRssSource(RssSourceDto rd)
        {
            return await Task.Run(async () =>
            {
                var rss = new RssSource()
                {
                    Id = rd.Id,
                    Name = rd.Name,
                    Url = rd.Url
                };
                await _unit.RssSources.Update(rss);
                return await _unit.SaveChangesAsync();
            });
        }

        public async Task<IEnumerable<RssSourceDto>> GetAllRssSources()
        {
            var rssDtos =  await _unit.RssSources.ReadMany(r 
                => !string.IsNullOrEmpty(r.Name))
                .Select(r => new RssSourceDto()
            {
                Id = r.Id,
                Name = r.Name,
                Url = r.Url
            }).ToListAsync();

            return rssDtos;
        }

        public Task<NewsWithRssSourceNameDto> GetNewsWithRssSourceNameById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<RssSourceDto> GetRssSourceById(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
