﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoNews.Models
{
    public class PageInfo
    {
        public int PageNumber { get; set; }
        public int ItemsOnPage { get; set; }
        public int CountItems { get; set; }

        public int CountPages => CountItems / ItemsOnPage + 1;
    }
}
