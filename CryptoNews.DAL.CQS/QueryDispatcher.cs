using CryptoNews.DAL.CQS.Queries;
using CryptoNews.DAL.CQS.QueryHandlers;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CryptoNews.DAL.CQS
{
    public class QueryDispatcher : IQueryDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public QueryDispatcher(IServiceProvider svcProvider)
        {
            _serviceProvider = svcProvider ??
                throw new ArgumentNullException(null, "serviceProvider");
        }


        public Task<TR> Handle<TQ, TR>(TQ query, CancellationToken token) where TQ : IQuery<TR>
        {
            var handlerType = typeof(IQueryHandler<,>)
                .MakeGenericType(query.GetType(), typeof(TR));
            try
            {
                dynamic handler = _serviceProvider.GetService(handlerType);
                return handler.Handler((dynamic)query, token);
            }
            catch (Exception ex)
            {
                throw new HandlerNotFoundException(ex);
            }
        }
    }
}
