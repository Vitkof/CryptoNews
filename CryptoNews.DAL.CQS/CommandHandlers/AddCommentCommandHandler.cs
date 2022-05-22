using AutoMapper;
using CryptoNews.DAL.CQS.Commands;
using CryptoNews.DAL.Entities;
using System.Threading;

namespace CryptoNews.DAL.CQS.CommandHandlers
{
    public class AddCommentCommandHandler : ICommandHandler<AddCommentCommand>
    {
        private readonly CryptoNewsContext _context;
        private readonly IMapper _mapper;

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
