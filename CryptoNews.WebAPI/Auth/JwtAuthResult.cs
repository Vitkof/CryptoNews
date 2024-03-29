﻿using System;

namespace CryptoNews.WebAPI.Auth
{
    public class JwtAuthResult
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
