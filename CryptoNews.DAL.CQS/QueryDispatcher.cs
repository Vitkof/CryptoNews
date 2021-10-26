using CryptoNews.DAL.CQS.Queries;
using CryptoNews.DAL.CQS.QueryHandlers;
using System;
using System.Threading.Tasks;
using System.Web.Http.Dependencies;

namespace CryptoNews.DAL.CQS
{
    public class QueryDispatcher : IQueryDispatcher
    {
        private readonly IDependencyResolver _resolver;

        public QueryDispatcher(IDependencyResolver resolver)
        {
            _resolver = resolver;
        }
        /*
        public IQueryHandler<TQ, TR> Dispatch(TQ query)
        {
            IOCContainer.GetByType()
        }*/

        public Task<TR> Handle<TQ, TR>(TQ query) where TQ : IQuery<TR>
        {
            if (query == null) throw new ArgumentNullException(nameof(query));

            var handler = _resolver.Resolve<IQueryHandler<TQ, TR>>();
        }
    }
}
