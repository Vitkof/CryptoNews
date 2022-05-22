using System.Threading;
using System.Threading.Tasks;
using CryptoNews.DAL.CQS.Queries.User;
using CryptoNews.DAL.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CryptoNews.DAL.CQS.QueryHandlers.User
{
    public class CheckAuthenticationQueryHandler : IRequestHandler<CheckAuthenticationQuery, bool>
    {
        private readonly CryptoNewsContext _context;

        public CheckAuthenticationQueryHandler(CryptoNewsContext context)
        {
            _context = context;
        }


        public async Task<bool> Handle(CheckAuthenticationQuery request, CancellationToken token)
        {
            return await _context.Users.AnyAsync(
                u => u.Email.Equals(request.Email) && u.PasswordHash.Equals(request.PasswordHash),
                cancellationToken: token);
        }
    }
}