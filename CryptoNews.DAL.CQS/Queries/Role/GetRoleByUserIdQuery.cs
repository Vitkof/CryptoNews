using CryptoNews.Core.DTO;
using System;


namespace CryptoNews.DAL.CQS.Queries.Role
{
    public class GetRoleByUserIdQuery : IQuery<RoleDto>
    {
        public Guid UserId { get; set; }
    }
}
