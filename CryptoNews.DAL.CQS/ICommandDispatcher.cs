using CryptoNews.DAL.CQS.Commands;
using System.Threading;

namespace CryptoNews.DAL.CQS
{
    public interface ICommandDispatcher
    {
        void Handle<TCommand>(TCommand command, CancellationToken token)
            where TCommand : ICommand;
    }
}
