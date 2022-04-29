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
    public class UpdateRefreshTokenCommandHandler : IRequestHandler<UpdateRefreshTokenCommand, int>
    {
        private readonly CryptoNewsContext _context;
        private readonly IMapper _mapper;

        public UpdateRefreshTokenCommandHandler(IMapper mapper,
                                                CryptoNewsContext db)
        {
            _mapper = mapper;
            _context = db;
        }

        public async Task<int> Handle(UpdateRefreshTokenCommand request, 
                                      CancellationToken cancellationToken)
        {

            var currentRTs = await _context.RefreshTokens
                .AsNoTracking()
                .Where(rt => rt.UserId.Equals(request.UserId))
                .ToListAsync(cancellationToken);

            _context.RefreshTokens.RemoveRange(currentRTs);
            await _context.SaveChangesAsync(cancellationToken);

            var newRT = _mapper.Map<RefreshToken>(request.NewRefreshToken);
            await _context.RefreshTokens.AddAsync(newRT, cancellationToken);

            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
