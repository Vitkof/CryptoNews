using System;

namespace CryptoNews.Core.DTO
{
    public abstract class Dto : IDto
    {
        public Guid Id { get; set; }
    }
}
