using AutoMapper;
using CryptoNews.DAL.CQS.Commands;
using CryptoNews.DAL.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CryptoNews.DAL.CQS.CommandHandlers
{
    public class RevokeRefreshTokenCommandHandler : IRequestHandler<RevokeRefreshTokenCommand, int>
    {
        private readonly CryptoNewsContext _context;

        public RevokeRefreshTokenCommandHandler(CryptoNewsContext db)
        {
            _context = db;
        }

        public async Task<int> Handle(RevokeRefreshTokenCommand request, 
                                      CancellationToken cancellationToken)
        {
            _context.RefreshTokens.Remove(await _context.RefreshTokens.FirstOrDefaultAsync(rt =>
                rt.Id.Equals(request.TokenId), cancellationToken));

            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
