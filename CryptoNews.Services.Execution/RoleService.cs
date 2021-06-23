using CryptoNews.Core.DTO;
using CryptoNews.Core.IServices;
using CryptoNews.DAL.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoNews.Services.Implement
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unit;

        public RoleService(IUnitOfWork uow)
        {
            _unit = uow;
        }

        public async Task AddRoleToUser(Guid userId, RoleDto rd)
        {
            if(_unit.Roles.ReadById(rd.Id) != null)
            {
                var user = _unit.Users.ReadById(userId);
                user.RoleId = rd.Id;
                await _unit.Users.Update(user);
                await _unit.SaveChangesAsync();
            }
        }

        public RoleDto GetRoleByUserId(Guid userId)
        {
            var role = _unit.Users
                .ReadMany(u=> u.Id==userId, 
                includes: u=>u.Role)
                .FirstOrDefault().Role;

            return new RoleDto()
            {
                Id = role.Id,
                Name = role.Name
            };
        }

        public IEnumerable<RoleDto> GetRoles()
        {
            return _unit.Roles.ReadAll()
                .Select(r => new RoleDto()
                {
                    Id = r.Id,
                    Name = r.Name
                })
                .ToList();
        }


        public Task<int> DeleteRole(RoleDto ud)
        {
            throw new NotImplementedException();
        }

        public Task<int> EditRole(RoleDto ud)
        {
            throw new NotImplementedException();
        }

        

        
    }
}
