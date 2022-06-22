using System;

namespace CryptoNews.DAL.CQS.Queries.Role
{
    public class GetIdByNameQuery : IQuery<Guid>
    {
        public string Name { get; set; }
    }
}
