using AutoMapper;
using CryptoNews.DAL.CQS.Commands;
using CryptoNews.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace CryptoNews.DAL.CQS.CommandHandlers
{
    public class EditUserCommandHandler : ICommandHandler<EditUserCommand>
    {
        private readonly CryptoNewsContext _context;
        private readonly IMapper _mapper;

        public EditUserCommandHandler(CryptoNewsContext context, IMapper map)
        {
            _context = context;
            _mapper = map;
        }

        public async void Handle(EditUserCommand cmd, CancellationToken token)
        {
            var userEntity = await _context.Users
                .FirstOrDefaultAsync(u => u.Id.Equals(cmd.User.Id), token);

            userEntity = _mapper.Map<User>(cmd.User);
            await _context.SaveChangesAsync(token);
        }
    }
}
