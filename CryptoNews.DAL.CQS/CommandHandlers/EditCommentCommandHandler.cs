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
    public class EditCommentCommandHandler : ICommandHandler<EditCommentCommand>
    {
        private CryptoNewsContext _context;

        public EditCommentCommandHandler(CryptoNewsContext context)
        {
            _context = context;
        }

        public async void Handle(EditCommentCommand cmd, CancellationToken token)
        {
            var commentEntity = await _context.Comments
                .FirstOrDefaultAsync(c => c.Id.Equals(cmd.Comment.Id), token);

            commentEntity.Text = cmd.Comment.Text;
            await _context.SaveChangesAsync(token);
        }
    }
}
