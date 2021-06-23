using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoNews.Policies
{
    public class MinimalAgeReq : IAuthorizationRequirement
    {
        public byte MinAge { get; set; }
        public MinimalAgeReq(byte minimal)
        {
            MinAge = minimal;
        }
    }
}
