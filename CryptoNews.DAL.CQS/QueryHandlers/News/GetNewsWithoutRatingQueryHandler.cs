using AutoMapper;
using CryptoNews.Core.DTO;
using CryptoNews.DAL.CQS.Queries.News;
using CryptoNews.DAL.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CryptoNews.DAL.CQS.QueryHandlers.News
{
    public class GetNewsWithoutRatingQueryHandler : IRequestHandler<GetNewsWithoutRatingQuery, IEnumerable<NewsDto>>
    {
        private readonly CryptoNewsContext _context;
        private readonly IMapper _mapper;

        public GetNewsWithoutRatingQueryHandler(CryptoNewsContext context, IMapper map)
        {
            _context = context;
            _mapper = map;
        }

        public async Task<IEnumerable<NewsDto>> Handle(GetNewsWithoutRatingQuery query, CancellationToken token)
        {
            return await _context.News
                .Where(n => n.Rating == 0)
                .Take(30)
                .Select(n => _mapper.Map<NewsDto>(n))
                .ToListAsync(token);
        }
    }
}
