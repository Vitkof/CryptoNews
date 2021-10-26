using CryptoNews.DAL.CQS.Queries;
using CryptoNews.DAL.CQS.QueryHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoNews.DAL.CQS
{
    public class QueryBus
    {
        public object Resolve<THandler, TQuery, TResult>(TQuery query) 
            where THandler : IQueryHandler<TQuery, TResult>, IQueryHandler, new()
            where TResult : class
            where TQuery : class
        {
            return new THandler().Handle(query);
        }
        /*
        public object Resolve<T, TQuery>(TQuery query)
            where T : IQueryHandler<TQuery, object>, new()
            where TQuery : class, IQuery<T>
        {
            return new T().Handle(query);
        }*/
    }
}
