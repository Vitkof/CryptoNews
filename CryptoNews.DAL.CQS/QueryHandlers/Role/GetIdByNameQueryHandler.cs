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
    public class GetIdByNameQueryHandler : IQueryHandler<GetIdByNameQuery, Guid>
    {
        private readonly CryptoNewsContext _context;

        public GetIdByNameQueryHandler(CryptoNewsContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(GetIdByNameQuery query, CancellationToken token)
        {
            var roleId = (await _context.Roles
                .FirstOrDefaultAsync(r => r.Name.Equals(query.Name), token))
                .Id;

            return roleId;
        }
    }
}
