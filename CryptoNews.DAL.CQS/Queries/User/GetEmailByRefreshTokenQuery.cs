namespace CryptoNews.DAL.CQS.Queries.User
{
    public class GetEmailByRefreshTokenQuery : IQuery<string>
    {
        public string Token { get; set; }
    }
}
