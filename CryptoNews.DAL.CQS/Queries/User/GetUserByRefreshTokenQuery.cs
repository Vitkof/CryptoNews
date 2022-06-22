using CryptoNews.Core.DTO;

namespace CryptoNews.DAL.CQS.Queries.User
{
    public class GetUserByRefreshTokenQuery : IQuery<UserDto>
    {
        public string Token { get; set; }
    }
}
