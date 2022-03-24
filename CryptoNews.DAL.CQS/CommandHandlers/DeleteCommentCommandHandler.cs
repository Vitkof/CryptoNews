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
    public class DeleteCommentCommandHandler : ICommandHandler<DeleteCommentCommand>
    {
        private CryptoNewsContext _context;

        public DeleteCommentCommandHandler(CryptoNewsContext context)
        {
            _context = context;
        }

        public async void Handle(DeleteCommentCommand cmd, CancellationToken token)
        {
            Comment commentEntity = await _context.Comments
                .FirstOrDefaultAsync(c => c.Id.Equals(cmd.Id), token);
            _context.Comments.Remove(commentEntity);
            await _context.SaveChangesAsync(token);
        }
    }
}
