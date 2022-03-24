using CryptoNews.DAL.CQS.Commands;
using CryptoNews.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CryptoNews.DAL.CQS.CommandHandlers
{
    public class ReplaceUserRoleCommandHandler : ICommandHandler<ReplaceUserRoleCommand>
    {
        private readonly CryptoNewsContext _context;

        public ReplaceUserRoleCommandHandler(CryptoNewsContext context)
        {
            _context = context;
        }

        public async void Handle(ReplaceUserRoleCommand cmd, CancellationToken token)
        {
            (await _context.Users
                .FirstOrDefaultAsync(u => u.Id.Equals(cmd.UserId), token))
                .RoleId = cmd.Role.Id;

            await _context.SaveChangesAsync(token);
        }
    }
}
