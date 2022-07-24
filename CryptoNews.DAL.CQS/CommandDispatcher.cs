using CryptoNews.DAL.CQS.CommandHandlers;
using CryptoNews.DAL.CQS.Commands;
using System;
using System.Threading;

namespace CryptoNews.DAL.CQS
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public CommandDispatcher(IServiceProvider svcProvider)
        {
            _serviceProvider = svcProvider ??
                throw new ArgumentNullException(nameof(svcProvider));
        }


        public void Handle<TCmd>(TCmd command, CancellationToken token)
            where TCmd : ICommand
        {
            if (command == null)
                throw new ArgumentNullException(nameof(command));

            var handlerType = typeof(ICommandHandler<TCmd>);

            try
            {
                dynamic handler = _serviceProvider.GetService(handlerType);
                handler.Handle((dynamic)command, token);
            }
            catch (Exception ex)
            {
                throw new HandlerNotFoundException(ex);
            }
        }
    }
}
