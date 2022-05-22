using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CryptoNews.Core.DTO;

namespace CryptoNews.Core.IServices
{
    public interface IRoleService
    {
        RoleDto GetRoleByUserId(Guid userId);
        string GetRoleNameByEmail(string email);
        IEnumerable<RoleDto> GetRoles();
        Task AddRoleToUser(Guid userId, RoleDto rd);
    }
}
