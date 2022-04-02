using CryptoNews.DAL.CQS.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleInjector;
using System.Threading;

namespace CryptoNews.DAL.CQS.Test
{
    [TestClass]
    public class QueryDispatcherTest
    {
        [TestMethod]
        [ExpectedException(typeof(HandlerNotFoundException))]
        public void HandlerNotFoundExceptionTest()
        {
            var container = new Container();
            container.Register<IQueryDispatcher>(() => new QueryDispatcher(container));

            var queryDispatcher = container.GetInstance<IQueryDispatcher>();
            queryDispatcher.Handle<NewsQuery, News>(new NewsQuery(), new CancellationToken());
        }

        class News { }
        class NewsQuery : IQuery<News> { }
    }
}
