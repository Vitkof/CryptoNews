using CryptoNews.Core.DTO;
using CryptoNews.Core.IServices;
using CryptoNews.DAL.Entities;
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
                _unit.Users.Update(user);
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


        public async Task<int> DeleteRole(RoleDto rd)
        {
            _unit.Roles.Delete(rd.Id);
            return await _unit.SaveChangesAsync();
        }

        public async Task<int> EditRole(RoleDto rd)
        {
            var role = new Role
            {
                Id = rd.Id,
                Name = rd.Name
            };
            _unit.Roles.Update(role);
            return await _unit.SaveChangesAsync();
        }
    }
}
