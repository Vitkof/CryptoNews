using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoNews.Models.ViewModels
{
    public class CommentCreateVM
    {
        public Guid NewsId { get; set; }
        [StringLength(1000)]
        public string CommText { get; set; }
    }
}
