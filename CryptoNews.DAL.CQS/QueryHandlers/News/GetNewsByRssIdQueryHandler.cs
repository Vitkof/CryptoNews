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
    public class GetNewsByRssIdQueryHandler : IQueryHandler<GetNewsByRssIdQuery, IEnumerable<NewsDto>>
    {
        private readonly CryptoNewsContext _context;
        private readonly IMapper _mapper;

        public GetNewsByRssIdQueryHandler(CryptoNewsContext context, IMapper map)
        {
            _context = context;
            _mapper = map;
        }

        public async Task<IEnumerable<NewsDto>> Handle(GetNewsByRssIdQuery query, CancellationToken token)
        {
            return
                await _context.News
                .Where(n => n.RssSourceId.Equals(query.RssId))
                .Select(n => _mapper.Map<NewsDto>(n))
                .ToListAsync(token);
        }
    }
}
