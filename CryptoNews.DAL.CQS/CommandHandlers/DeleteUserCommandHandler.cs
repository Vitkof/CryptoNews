using CryptoNews.DAL.CQS.Commands;
using CryptoNews.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace CryptoNews.DAL.CQS.CommandHandlers
{
    public class DeleteUserCommandHandler : ICommandHandler<DeleteUserCommand>
    {
        private readonly CryptoNewsContext _context;

        public DeleteUserCommandHandler(CryptoNewsContext context)
        {
            _context = context;
        }

        public async void Handle(DeleteUserCommand cmd, CancellationToken token)
        {
            User userEntity = await _context.Users
                .FirstOrDefaultAsync(u => u.Id.Equals(cmd.Id), token);
            _context.Users.Remove(userEntity);
            await _context.SaveChangesAsync(token);
        }
    }
}
