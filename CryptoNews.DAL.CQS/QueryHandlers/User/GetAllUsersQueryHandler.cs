using AutoMapper;
using CryptoNews.Core.DTO;
using CryptoNews.DAL.CQS.Queries.User;
using CryptoNews.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CryptoNews.DAL.CQS.QueryHandlers.User
{
    public class GetAllUsersQueryHandler : IQueryHandler<GetAllUsersQuery, IEnumerable<UserDto>>
    {
        private readonly CryptoNewsContext _context;
        private readonly IMapper _mapper;

        public GetAllUsersQueryHandler(CryptoNewsContext context, IMapper map)
        {
            _context = context;
            _mapper = map;
        }

        public async Task<IEnumerable<UserDto>> Handle(GetAllUsersQuery query, CancellationToken token)
        {
            return
                await _context.Users
                .Select(u => _mapper.Map<UserDto>(u))
                .ToListAsync(token);
        }
    }
}
