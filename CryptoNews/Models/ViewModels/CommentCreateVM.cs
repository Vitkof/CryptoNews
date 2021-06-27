using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoNews.Models.ViewModels
{
    public class CommentCreateVM
    {
        public Guid NewsId { get; set; }
        public string CommText { get; set; }
    }
}
