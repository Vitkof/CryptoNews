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
                throw new ArgumentNullException(nameof(svcProvider));
        }


        public async Task<TR> HandleAsync<TQ, TR>(TQ query, CancellationToken token = default) where TQ : IQuery<TR>
        {
            var handler = GetHandler<IQueryHandler<TQ, TR>, TQ>(query);

            return await handler.Handle((dynamic)query, token);
        }

        public TR Dispatch<TQ, TR>(TQ query, CancellationToken token = default) where TQ : IQuery<TR>
        {
            var handler = GetHandler<IQueryHandler<TQ, TR>, TQ>(query);

            return handler.Handle((dynamic)query, token).Result;
        }

        #region private
        private THandler GetHandler<THandler, TQuery>(TQuery query)
        {
            if (query == null)
                throw new ArgumentNullException(nameof(query));

            var handlerType = typeof(THandler);

            try
            {
                dynamic handler = _serviceProvider.GetService(handlerType);
                return handler;
            }
            catch(Exception ex)
            {
                throw new HandlerNotFoundException(ex);
            }
        }
        #endregion
    }
}
