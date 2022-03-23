using CryptoNews.DAL.CQS.Queries;
using System.Threading;
using System.Threading.Tasks;

namespace CryptoNews.DAL.CQS.QueryHandlers
{
    public interface IQueryHandler<TQ, TR> 
        where TQ : IQuery<TR>
    {
        Task<TR> Handle(TQ query, CancellationToken token);
    }
}
