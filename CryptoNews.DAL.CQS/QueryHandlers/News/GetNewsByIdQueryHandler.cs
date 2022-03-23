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
    public class GetNewsByIdQueryHandler : IQueryHandler<GetNewsByIdQuery, NewsDto>
    {
        private readonly CryptoNewsContext _context;
        private readonly IMapper _mapper;

        public GetNewsByIdQueryHandler(CryptoNewsContext context, IMapper map)
        {
            _context = context;
            _mapper = map;
        }

        public async Task<NewsDto> Handle(GetNewsByIdQuery query, CancellationToken token)
        {
            return
                _mapper.Map<NewsDto>(await _context.News
                .FirstOrDefaultAsync(n => n.Id.Equals(query.Id), token));
        }
    }
}
