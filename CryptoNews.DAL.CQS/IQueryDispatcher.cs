using CryptoNews.DAL.CQS.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CryptoNews.DAL.CQS
{
    public interface IQueryDispatcher
    {
        Task<TR> Handle<TQ, TR>(TQ query, CancellationToken token)
            where TQ : IQuery<TR>;
    }
}
