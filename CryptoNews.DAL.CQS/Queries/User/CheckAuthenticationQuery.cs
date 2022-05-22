using MediatR;

namespace CryptoNews.DAL.CQS.Queries.User
{
    public class CheckAuthenticationQuery : IRequest<bool>
    {
        public string Email { get; set; }
        public string PasswordHash { get; set; }
    }
}