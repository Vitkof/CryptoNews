using CryptoNews.Core.DTO;
using MediatR;

namespace CryptoNews.DAL.CQS.Commands
{
    public class RegisterUserCommand : IRequest<int>
    {
        public UserDto User { get; set; }
    }
}
