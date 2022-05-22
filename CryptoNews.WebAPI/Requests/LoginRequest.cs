using System.ComponentModel.DataAnnotations;

namespace CryptoNews.WebAPI.Requests
{
    public class LoginRequest
    {
        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
