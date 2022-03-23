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
    public class GetAllRolesQueryHandler : IQueryHandler<GetAllRolesQuery, IEnumerable<RoleDto>>
    {
        private CryptoNewsContext _context;
        private IMapper _mapper;

        public GetAllRolesQueryHandler(CryptoNewsContext context, IMapper map)
        {
            _context = context;
            _mapper = map;
        }

        public async Task<IEnumerable<RoleDto>> Handle(GetAllRolesQuery query, CancellationToken token)
        {
            return
                await _context.Roles
                .Select(r => _mapper.Map<RoleDto>(r))
                .ToListAsync(token);
        }
    }
}
