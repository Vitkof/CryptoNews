﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoNews.Core.DTO
{
    public class CommentDto : Dto
    {
        [StringLength(1000)]
        public string Text { get; set; }

        [DisplayFormat(DataFormatString = "{0:MMM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime CreateAt { get; set; }
        public bool Active { get; set; }
        public ushort Rating { get; set; }

        public Guid NewsId { get; set; }
        public Guid UserId { get; set; }
        public Guid? ParentId { get; set; }
    }
}
