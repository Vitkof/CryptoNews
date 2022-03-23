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
    public class GetUserByIdQueryHandler : IQueryHandler<GetUserByIdQuery, UserDto>
    {
        private readonly CryptoNewsContext _context;
        private readonly IMapper _mapper;

        public GetUserByIdQueryHandler(CryptoNewsContext context, IMapper map)
        {
            _context = context;
            _mapper = map;
        }

        public async Task<UserDto> Handle(GetUserByIdQuery query, CancellationToken token)
        {
            return
                _mapper.Map<UserDto>(await _context.Users
                .FirstOrDefaultAsync(u => u.Id.Equals(query.Id),
                    cancellationToken: token));
        }
    }
}
