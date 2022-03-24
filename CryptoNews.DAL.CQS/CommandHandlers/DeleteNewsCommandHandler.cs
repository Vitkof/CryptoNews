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
    public class DeleteNewsCommandHandler : ICommandHandler<DeleteNewsCommand>
    {
        private readonly CryptoNewsContext _context;

        public DeleteNewsCommandHandler(CryptoNewsContext context)
        {
            _context = context;
        }

        public async void Handle(DeleteNewsCommand cmd, CancellationToken token)
        {
            News newsEntity = await _context.News
                .FirstOrDefaultAsync(n => n.Id.Equals(cmd.Id), token);
            _context.News.Remove(newsEntity);
            await _context.SaveChangesAsync(token);
        }
    }
}
