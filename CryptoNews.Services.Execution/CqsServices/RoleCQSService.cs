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

        public Task AddRoleToUser(Guid userId, RoleDto rd)
        {
            throw new NotImplementedException();
        }

        public void ReplaceUserRole(Guid userId, RoleDto role)
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
