using AutoMapper;
using CryptoNews.Core.DTO;
using CryptoNews.DAL.CQS.Queries;
using CryptoNews.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoNews.DAL.CQS.QueryHandlers
{
    public class GetRssByIdQueryHandler : IQueryHandler<GetRssByIdQuery, RssSourceDto>
    {
        //private readonly IQueryHandler<GetRssByIdQuery, RssSourceDto> _decor;
        private readonly CryptoNewsContext _context;
        private readonly IMapper _mapper;

        public GetRssByIdQueryHandler(CryptoNewsContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            //_decor = decorated;
        }

        public async Task<RssSourceDto> Handle(GetRssByIdQuery query)
        {
            return _mapper.Map<RssSourceDto>(await _context.RssSources.FirstOrDefaultAsync(src => src.Id.Equals(query.Id)));
        }
    }
}
