using CryptoNews.Core.DTO;
using System.Collections.Generic;


namespace CryptoNews.DAL.CQS.Queries.User
{
    public class GetAllUsersQuery : IQuery<IEnumerable<UserDto>>
    {
    }
}
