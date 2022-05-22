using System.ComponentModel.DataAnnotations;

namespace CryptoNews.WebAPI.Requests
{
    public class RegisterRequest
    {
        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Password mismatch.")]
        public string PasswordConfirmation { get; set; }
    }
}
