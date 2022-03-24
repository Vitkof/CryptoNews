using CryptoNews.Core.DTO;

namespace CryptoNews.DAL.CQS.Commands
{
    public class AddCommentCommand
    {
        public CommentDto Comment { get; set; }
    }
}
