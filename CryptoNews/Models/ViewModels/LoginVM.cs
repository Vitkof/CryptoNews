using System.ComponentModel.DataAnnotations;

namespace CryptoNews.Models.ViewModels
{
    public class LoginVM
    {
        [Required(ErrorMessage = "fill in your email address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "fill in your password")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = 
            "Minimum length of 6 and a maximum length of 20.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [StringLength(4)]
        [Display(Name ="Captcha")]
        public string CaptchaCode { get; set; }

        public string ReturnUrl { get; set; }
    }
}
