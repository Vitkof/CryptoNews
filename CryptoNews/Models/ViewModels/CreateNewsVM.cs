using CryptoNews.Core.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoNews.Models.ViewModels
{
    public class CreateNewsVM
    {
        public Guid Id { get; set; }
        [StringLength(150, MinimumLength = 3), Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        [Display(Name ="Весь текст")]
        public string Body { get; set; }

        [DisplayFormat(DataFormatString = "{0:MMM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime PubDate { get; set; }

        public float Rating { get; set; }
        [Url]
        public string Url { get; set; }

        public Guid RssSourceId { get; set; }

        public SelectList RssList { get; set; }
    }
}
