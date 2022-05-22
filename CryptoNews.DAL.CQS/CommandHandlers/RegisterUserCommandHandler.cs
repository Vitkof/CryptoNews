using AutoMapper;
using CryptoNews.DAL.CQS.Commands;
using CryptoNews.DAL.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CryptoNews.DAL.CQS.CommandHandlers
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, int>
    {
        private readonly CryptoNewsContext _context;
        private readonly IMapper _mapper;

        public RegisterUserCommandHandler(CryptoNewsContext context, IMapper map)
        {
            _context = context;
            _mapper = map;
        }

        public async Task<int> Handle(RegisterUserCommand request, CancellationToken token)
        {
            await _context.Users.AddAsync(_mapper.Map<User>(request.User), token);
            return await _context.SaveChangesAsync(token);
        }
    }
}
