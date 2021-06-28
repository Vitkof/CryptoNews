using System.ComponentModel.DataAnnotations;

namespace CryptoNews.Models.ViewModels
{
    public class UserVM
    {
        [Required(ErrorMessage = "fill in your email address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string FullName { get; set; }
    }
}
