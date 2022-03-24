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
    public class AddNewsCommandHandler : ICommandHandler<AddNewsCommand>
    {
        private readonly CryptoNewsContext _context;

        public AddNewsCommandHandler(CryptoNewsContext context)
        {
            _context = context;
        }

        public async void Handle(AddNewsCommand cmd, CancellationToken token)
        {
            var news = new News()
            {
                Id = cmd.Id,
                Title = cmd.Title,
                Description = cmd.Description,
                Body = cmd.Body,
                Rating = cmd.Rating,
                RssSourceId = cmd.RssSourceId,
                Url = cmd.Url,
                PubDate = cmd.PubDate
            };
            //automapper here next time
            await _context.News.AddAsync(news, token);
            await _context.SaveChangesAsync(token);
        }
    }
}
