using CryptoNews.DAL.CQS.Commands;
using CryptoNews.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoNews.DAL.CQS.CommandHandlers
{
    public class AddNewsCommandHandler : ICommandHandler<AddNewsCommand>
    {
        private readonly CryptoNewsContext _context;

        public void Handle(AddNewsCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
