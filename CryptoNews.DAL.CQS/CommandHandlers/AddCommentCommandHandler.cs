using AutoMapper;
using CryptoNews.DAL.CQS.Commands;
using CryptoNews.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CryptoNews.DAL.CQS.CommandHandlers
{
    public class AddCommentCommandHandler : ICommandHandler<AddCommentCommand>
    {
        private CryptoNewsContext _context;
        private IMapper _mapper;

        public AddCommentCommandHandler(CryptoNewsContext context, IMapper map)
        {
            _context = context;
            _mapper = map;
        }

        public async void Handle(AddCommentCommand cmd, CancellationToken token)
        {
            await _context.Comments.AddAsync(
                _mapper.Map<Comment>(cmd.Comment), token);
            await _context.SaveChangesAsync(token);
        }
    }
}
