using CryptoNews.DAL.CQS.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleInjector;
using System.Threading;

namespace CryptoNews.DAL.CQS.Test
{
    [TestClass]
    public class CommandDispatcherTest
    {
        [TestMethod]
        [ExpectedException(typeof(HandlerNotFoundException))]
        public void HandlerNotFoundExceptionTest()
        {
            var container = new Container();
            container.Register<ICommandDispatcher>(() => new CommandDispatcher(container));

            var cmdDispatcher = container.GetInstance<ICommandDispatcher>();
            cmdDispatcher.Handle(new NewsCommand(), new CancellationToken());
        }

        class NewsCommand : ICommand { }
    }
}
