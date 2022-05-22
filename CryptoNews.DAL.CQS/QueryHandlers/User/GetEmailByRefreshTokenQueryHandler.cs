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
    public class GetEmailByRefreshTokenQueryHandler : IQueryHandler<GetEmailByRefreshTokenQuery, string>
    {
        private readonly CryptoNewsContext _context;

        public GetEmailByRefreshTokenQueryHandler(CryptoNewsContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(GetEmailByRefreshTokenQuery query, CancellationToken token)
        {
            var refreshTokenUserId = (await _context.RefreshTokens
                .AsNoTracking()
                .FirstOrDefaultAsync(
                    rt => rt.Token.Equals(query.Token), token)
                ).UserId;

            var email = (await _context.Users
                .AsNoTracking().FirstOrDefaultAsync(
                    u => u.Id.Equals(refreshTokenUserId), token)
                ).Email;

            return email;
        }
    }
}
