using CryptoNews.Core.DTO;
using System;


namespace CryptoNews.DAL.CQS.Queries.User
{
    public class GetUserByIdQuery : IQuery<UserDto>
    {
        public Guid Id { get; set; }
    }
}
