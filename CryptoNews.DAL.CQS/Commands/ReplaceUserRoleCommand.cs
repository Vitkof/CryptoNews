using CryptoNews.Core.DTO;
using System;

namespace CryptoNews.DAL.CQS.Commands
{
    public class ReplaceUserRoleCommand : ICommand
    {
        public Guid UserId { get; set; }
        public RoleDto Role { get; set; }
    }
}
