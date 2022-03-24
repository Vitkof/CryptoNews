using AutoMapper;
using CryptoNews.DAL.CQS.Commands;
using CryptoNews.DAL.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CryptoNews.DAL.CQS.CommandHandlers
{
    public class AddRangeNewsCommandHandler : IRequestHandler<AddRangeNewsCommand, int>
    {
        private readonly CryptoNewsContext _context;
        private readonly IMapper _mapper;

        public AddRangeNewsCommandHandler(CryptoNewsContext context, IMapper map)
        {
            _context = context;
            _mapper = map;
        }

        public async Task<int> Handle(AddRangeNewsCommand request, CancellationToken token)
        {
            await _context.News.AddRangeAsync(request.News
                .Select(n => _mapper.Map<News>(n)), token);

            return await _context.SaveChangesAsync(token);
        }
    }
}
