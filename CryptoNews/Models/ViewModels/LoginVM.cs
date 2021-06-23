using System.ComponentModel.DataAnnotations;

namespace CryptoNews.Models.ViewModels
{
    public class LoginVM
    {
        [Required(ErrorMessage = "fill in your email address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "fill in your password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string ReturnUrl { get; set; }
    }
}
