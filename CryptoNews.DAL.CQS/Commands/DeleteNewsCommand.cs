using System;

namespace CryptoNews.DAL.CQS.Commands
{
    public class DeleteNewsCommand
    {
        public Guid Id { get; set; }
    }
}
