using CryptoNews.DAL.CQS.Queries;
using System.Threading;
using System.Threading.Tasks;

namespace CryptoNews.DAL.CQS
{
    public interface IQueryDispatcher
    {
        Task<TR> HandleAsync<TQ, TR>(TQ query, CancellationToken token)
            where TQ : IQuery<TR>;
        TR Dispatch<TQ, TR>(TQ query, CancellationToken token)
            where TQ : IQuery<TR>;
    }
}
