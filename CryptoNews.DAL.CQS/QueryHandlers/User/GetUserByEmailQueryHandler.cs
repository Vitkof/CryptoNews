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
    public class GetUserByEmailQueryHandler : IQueryHandler<GetUserByEmailQuery, UserDto>
    {
        private readonly CryptoNewsContext _context;
        private readonly IMapper _mapper;

        public GetUserByEmailQueryHandler(CryptoNewsContext context, IMapper map)
        {
            _context = context;
            _mapper = map;
        }

        public async Task<UserDto> Handle(GetUserByEmailQuery query, CancellationToken token)
        {
            return
                _mapper.Map<UserDto>(await _context.Users
                .FirstOrDefaultAsync(u => u.Email.Equals(query.Email),
                    cancellationToken: token));
        }
    }
}
