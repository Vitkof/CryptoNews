using CryptoNews.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoNews.Models.ViewModels
{
    public class RegisterVM
    {
        private const string MSG = "field is required";

        [Required(ErrorMessage = MSG)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = MSG)]
        public string LastName { get; set; }

        [Required(ErrorMessage = MSG)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = MSG)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "no match, password not confirmed ")]
        public string PasswordConfirm { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        public GenderType Gender { get; set; }
        public string ShortDescription { get; set; }
    }
}
