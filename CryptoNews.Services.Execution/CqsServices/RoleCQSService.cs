using CryptoNews.Core.DTO;
using CryptoNews.Core.IServices;
using CryptoNews.DAL.CQS;
using CryptoNews.DAL.CQS.Commands;
using CryptoNews.DAL.CQS.Queries.Role;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CryptoNews.Services.Implement.CqsServices
{
    public class RoleCQSService : IRoleService
    {
        private readonly IQueryDispatcher _queryDispatcher;
        private readonly ICommandDispatcher _commandDispatcher;

        public RoleCQSService(IQueryDispatcher dispatcher,
                              ICommandDispatcher com)
        {
            _queryDispatcher = dispatcher;
            _commandDispatcher = com;
        }

        public async Task AddRoleToUser(Guid userId, RoleDto role)
        {
            await Task.Run(()=>
                ReplaceUserRole(userId, role)
            );
        }

        private void ReplaceUserRole(Guid userId, RoleDto role)
        {
            try
            {
                var cmd = new ReplaceUserRoleCommand() 
                { UserId = userId, Role = role };
                _commandDispatcher.Handle(cmd, new CancellationToken());
                Log.Information($"Replace User role succeeded {userId}. {role.Name} ");
            }
            catch (Exception ex)
            {
                Log.Error($"ReplaceUserRole Error: {ex.Message} . {userId}");
            }
        }

        public RoleDto GetRoleByUserId(Guid userId)
        {
            try
            {
                var query = new GetRoleByUserIdQuery() 
                { UserId = userId };
                return _queryDispatcher
                    .Dispatch<GetRoleByUserIdQuery, RoleDto>(query, new CancellationToken());
            }
            catch(Exception ex)
            {
                Log.Error($"GetRoleByUserId Error: {ex.Message}");
                return null;
            }
        }

        public string GetRoleNameByEmail(string email)
        {
            try
            {
                var query = new GetRoleNameByEmailQuery()
                { Email = email };
                var roleName = _queryDispatcher
                    .Dispatch<GetRoleNameByEmailQuery, string>(query, new CancellationToken());
                return roleName;
            }
            catch(Exception ex)
            {
                Log.Error($"GetRoleNameByEmail Error: {ex.Message}");
                throw;
            }
        }

        public Guid GetIdByName(string name)
        {
            try
            {
                var query = new GetIdByNameQuery()
                { Name = name };
                var roleId = _queryDispatcher
                    .Dispatch<GetIdByNameQuery, Guid>(query, new CancellationToken());
                return roleId;
            }
            catch (Exception ex)
            {
                Log.Error($"GetRoleNameByEmail Error: {ex.Message}");
                throw;
            }
        }

        public IEnumerable<RoleDto> GetRoles()
        {
            try
            {
                var query = new GetAllRolesQuery();
                return _queryDispatcher
                    .Dispatch<GetAllRolesQuery, IEnumerable<RoleDto>>(query, new CancellationToken());
            }
            catch(Exception ex)
            {
                Log.Error($"GetRoles Error: {ex.Message}");
                return null;
            }
        }
    }
}
