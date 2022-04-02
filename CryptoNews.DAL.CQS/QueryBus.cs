using CryptoNews.DAL.CQS.Queries;
using CryptoNews.DAL.CQS.QueryHandlers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CryptoNews.DAL.CQS
{
    public class QueryBus
    {
        private readonly IMediator _mediator;
        public QueryBus(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<TResponse> Send<TResponse>(IRequest<TResponse> command, CancellationToken token)
        {
            return await _mediator.Send(command, token);
        }
    }
}
