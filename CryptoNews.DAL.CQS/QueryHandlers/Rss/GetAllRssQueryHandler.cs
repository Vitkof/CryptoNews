using AutoMapper;
using CryptoNews.Core.DTO;
using CryptoNews.DAL.CQS.Queries.Rss;
using CryptoNews.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CryptoNews.DAL.CQS.QueryHandlers.Rss
{
    public class GetAllRssQueryHandler : IQueryHandler<GetAllRssQuery, IEnumerable<RssSourceDto>>
    {
        private readonly CryptoNewsContext _context;
        private readonly IMapper _mapper;

        public GetAllRssQueryHandler(CryptoNewsContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RssSourceDto>> Handle(GetAllRssQuery query, CancellationToken token)
        {
            return
                await _context.RssSources
                .Select(src => _mapper.Map<RssSourceDto>(src))
                .ToListAsync(token);
        }
    }
}
