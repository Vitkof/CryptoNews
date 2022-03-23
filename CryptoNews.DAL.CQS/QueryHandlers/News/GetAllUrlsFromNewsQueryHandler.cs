using AutoMapper;
using CryptoNews.Core.DTO;
using CryptoNews.DAL.CQS.Queries.News;
using CryptoNews.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CryptoNews.DAL.CQS.QueryHandlers.News
{
    public class GetAllUrlsFromNewsQueryHandler : IQueryHandler<GetAllUrlsFromNewsQuery, IEnumerable<string>>
    {
        private readonly CryptoNewsContext _context;


        public GetAllUrlsFromNewsQueryHandler(CryptoNewsContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<string>> Handle(GetAllUrlsFromNewsQuery query, CancellationToken token)
        {
            return
                await _context.News
                .Select(n => n.Url)
                .ToListAsync(token);
        }
    }
}
