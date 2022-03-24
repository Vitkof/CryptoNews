using CryptoNews.Core.DTO;

namespace CryptoNews.DAL.CQS.Commands
{
    public class EditCommentCommand
    {
        public CommentDto Comment { get; set; }
    }
}
