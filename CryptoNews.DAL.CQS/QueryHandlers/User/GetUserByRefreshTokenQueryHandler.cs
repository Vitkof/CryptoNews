using AutoMapper;
using CryptoNews.Core.DTO;
using CryptoNews.DAL.CQS.Queries.User;
using CryptoNews.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CryptoNews.DAL.CQS.QueryHandlers.User
{
    public class GetUserByRefreshTokenQueryHandler : IQueryHandler<GetUserByRefreshTokenQuery, UserDto>
    {
        private readonly CryptoNewsContext _context;
        private readonly IMapper _mapper;

        public GetUserByRefreshTokenQueryHandler(CryptoNewsContext context,
                                                 IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UserDto> Handle(GetUserByRefreshTokenQuery query, CancellationToken token)
        {
            var refreshTokenUserId = (await _context.RefreshTokens
                .AsNoTracking()
                .FirstOrDefaultAsync(
                    rt => rt.Token.Equals(query.Token), token)
                ).UserId;

            var user = _mapper.Map<UserDto>(await _context.Users
                .FirstOrDefaultAsync(
                    u => u.Id.Equals(refreshTokenUserId), token));

            return user;
        }
    }
}
