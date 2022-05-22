using CryptoNews.Core.DTO;

namespace CryptoNews.DAL.CQS.Commands
{
    public class EditUserCommand
    {
        public UserDto User { get; set; }
    }
}
