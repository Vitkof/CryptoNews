using CryptoNews.DAL.CQS.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoNews.DAL.CQS.QueryHandlers
{
    public interface IQueryHandler<TQ, TR> 
        where TQ : IQuery<TR>
    {
        Task<TR> Handle(TQ query);
    }
}
