﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoNews.DAL.Entities
{
    public interface IBaseEntity
    { }

    public interface IBaseEntity<T> : IBaseEntity
    {
        T Id { get; set; } 
    }
}
