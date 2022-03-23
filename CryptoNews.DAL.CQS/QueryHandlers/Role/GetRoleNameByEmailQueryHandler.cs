using CryptoNews.DAL.CQS.Queries.Role;
using CryptoNews.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CryptoNews.DAL.CQS.QueryHandlers.Role
{
    public class GetRoleNameByEmailQueryHandler : IQueryHandler<GetRoleNameByEmailQuery, string>
    {
        private readonly CryptoNewsContext _context;

        public GetRoleNameByEmailQueryHandler(CryptoNewsContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(GetRoleNameByEmailQuery query, CancellationToken token)
        {
            var roleIdByEmail = _context.Users.AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email.Equals(query.Email), token)
                .Result.RoleId;

            return 
                (await _context.Roles
                .FirstOrDefaultAsync(r => r.Id.Equals(roleIdByEmail), token))
                .Name;
        }
    }
}
