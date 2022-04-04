using CryptoNews.DAL.CQS.Queries;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleInjector;
using System;
using System.Threading;

namespace CryptoNews.DAL.CQS.Test
{
    [TestClass]
    public class QueryDispatcherTest
    {
        [TestMethod]
        [ExpectedException(typeof(HandlerNotFoundException))]
        public async void HandlerNotFoundExceptionTest()
        {
            var container = new Container();
            container.Register<IQueryDispatcher>(() => new QueryDispatcher(container));

            var queryDispatcher = container.GetInstance<IQueryDispatcher>();
            await queryDispatcher.HandleAsync<NewsQuery, News>(new NewsQuery(), new CancellationToken());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async void ArgumentNullExceptionTest()
        {
            var container = new Container();
            container.Register<IQueryDispatcher>(() => new QueryDispatcher(container));

            var queryDispatcher = container.GetInstance<IQueryDispatcher>();
            await queryDispatcher.HandleAsync<NewsQuery, News>(null, new CancellationToken());
        }

        class News { }
        class NewsQuery : IQuery<News> { }
    }
}
