using AutoMapper;
using CryptoNews.Core.DTO;
using CryptoNews.DAL.CQS.Queries.Rss;
using CryptoNews.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;


namespace CryptoNews.DAL.CQS.QueryHandlers
{
    public class GetRssByNameUrlQueryHandler : IQueryHandler<GetRssByNameUrlQuery, RssSourceDto>
    {
        private readonly CryptoNewsContext _context;
        private readonly IMapper _mapper;

        public GetRssByNameUrlQueryHandler(CryptoNewsContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<RssSourceDto> Handle(GetRssByNameUrlQuery query, CancellationToken token)
        {
            return
                _mapper.Map<RssSourceDto>(await _context.RssSources
                .FirstOrDefaultAsync(src => src.Name.Equals(query.Name) 
                                    && src.Url.Equals(query.Url), token));
        }
    }
}
