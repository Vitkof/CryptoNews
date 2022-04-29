using AutoMapper;
using CryptoNews.Core.DTO;
using CryptoNews.DAL.CQS.Queries.RefreshToken;
using CryptoNews.DAL.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CryptoNews.DAL.CQS.QueryHandlers.RefreshToken
{
    public class GetRefreshTokenByTokenValueQueryHandler : IRequestHandler<GetRefreshTokenByTokenValueQuery, RefreshTokenDto>
    {
        private readonly IMapper _mapper;
        private readonly CryptoNewsContext _context;

        public GetRefreshTokenByTokenValueQueryHandler(IMapper mapper,
                                                       CryptoNewsContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<RefreshTokenDto> Handle(GetRefreshTokenByTokenValueQuery request,
                                                  CancellationToken cancellationToken)
        {
            return _mapper.Map<RefreshTokenDto>(
                await _context.RefreshTokens
                .AsNoTracking()
                .FirstOrDefaultAsync(rt => rt.Token.Equals(request.TokenValue), 
                cancellationToken));
        }
    }
}
