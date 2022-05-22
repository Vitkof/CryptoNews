namespace CryptoNews.WebAPI.Auth
{
    public class JwtAuthResult
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
