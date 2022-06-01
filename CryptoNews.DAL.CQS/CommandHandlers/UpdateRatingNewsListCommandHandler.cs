using AutoMapper;
using CryptoNews.DAL.CQS.Commands;
using CryptoNews.DAL.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace CryptoNews.DAL.CQS.CommandHandlers
{
    public class UpdateRatingNewsListCommandHandler : IRequestHandler<UpdateRatingNewsListCommand, int>
    {
        private readonly CryptoNewsContext _context;

        public UpdateRatingNewsListCommandHandler(CryptoNewsContext context,
                                            IMapper map)
        {
            _context = context;
        }

        public async Task<int> Handle(UpdateRatingNewsListCommand request, 
                                      CancellationToken token)
        {

            foreach(var dto in request.NewsDtos)
            {
                var news = await _context.News.FirstOrDefaultAsync(n => n.Id.Equals(dto.Id), token);
                news.Rating = dto.Rating;
            }

            return await _context.SaveChangesAsync(token);
        }
    }
}
