using AutoMapper;
using CryptoNews.Core.DTO;
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
    public class GetRoleByUserIdQueryHandler : IQueryHandler<GetRoleByUserIdQuery, RoleDto>
    {
        private readonly CryptoNewsContext _context;
        private readonly IMapper _mapper;

        public GetRoleByUserIdQueryHandler(CryptoNewsContext context, IMapper map)
        {
            _context = context;
            _mapper = map;
        }

        public async Task<RoleDto> Handle(GetRoleByUserIdQuery query, CancellationToken token)
        {
            var roleIdByUserId = _context.Users.AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id.Equals(query.UserId), token)
                .Result.RoleId;

            return 
                _mapper.Map<RoleDto>(await _context.Roles
                .FirstOrDefaultAsync(r => r.Id.Equals(roleIdByUserId), token));
        }
    }
}
