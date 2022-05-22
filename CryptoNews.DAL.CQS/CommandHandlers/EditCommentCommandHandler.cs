using CryptoNews.DAL.CQS.Commands;
using CryptoNews.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace CryptoNews.DAL.CQS.CommandHandlers
{
    public class EditCommentCommandHandler : ICommandHandler<EditCommentCommand>
    {
        private readonly CryptoNewsContext _context;

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
