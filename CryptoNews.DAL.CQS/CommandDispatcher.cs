using CryptoNews.DAL.CQS.CommandHandlers;
using CryptoNews.DAL.CQS.Commands;
using System;
using System.Threading;

namespace CryptoNews.DAL.CQS
{
    public interface ICommandDispatcher
    {
        void Handle<TCommand>(TCommand command, CancellationToken token)
            where TCommand : ICommand;
    }


    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public CommandDispatcher(IServiceProvider svcProvider)
        {
            _serviceProvider = svcProvider ??
                throw new ArgumentNullException(null, "serviceProvider");
        }


        public void Handle<TCmd>(TCmd command, CancellationToken token)
            where TCmd : ICommand
        {
            var handlerType = typeof(ICommandHandler<>)
                .MakeGenericType(command.GetType());

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
