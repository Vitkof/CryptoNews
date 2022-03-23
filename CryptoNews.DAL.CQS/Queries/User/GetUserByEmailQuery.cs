using CryptoNews.Core.DTO;


namespace CryptoNews.DAL.CQS.Queries.User
{
    public class GetUserByEmailQuery : IQuery<UserDto>
    {
        public string Email { get; set; }
    }
}
