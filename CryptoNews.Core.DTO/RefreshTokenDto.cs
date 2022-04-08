using System;

namespace CryptoNews.Core.DTO
{
    public class RefreshTokenDto : Dto
    {
        public DateTime ElapsesUtc { get; set; }
        public string Token { get; set; }
        public DateTime CreationTime { get; set; }
        public Guid UserId { get; set; }
    }
}
