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
    public class GetAllNewsQueryHandler : IQueryHandler<GetAllNewsQuery, IEnumerable<NewsDto>>
    {
        private readonly CryptoNewsContext _context;
        private readonly IMapper _mapper;

        public GetAllNewsQueryHandler(CryptoNewsContext context, IMapper map)
        {
            _context = context;
            _mapper = map;
        }

        public async Task<IEnumerable<NewsDto>> Handle(GetAllNewsQuery query, CancellationToken token)
        {
            return await _context.News
                .OrderByDescending(n => n.Rating)
                .Select(n => _mapper.Map<NewsDto>(n))
                .ToListAsync(token);
        }
    }
}
