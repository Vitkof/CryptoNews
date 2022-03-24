using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CryptoNews.DAL.CQS.CommandHandlers
{
    public interface ICommandHandler<T>
    {
        public void Handle(T cmd, CancellationToken token);
    }
}
