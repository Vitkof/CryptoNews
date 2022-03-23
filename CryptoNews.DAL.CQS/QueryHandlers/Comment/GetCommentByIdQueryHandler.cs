using AutoMapper;
using CryptoNews.Core.DTO;
using CryptoNews.DAL.CQS.Queries.Comment;
using CryptoNews.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CryptoNews.DAL.CQS.QueryHandlers.Comment
{
    public class GetCommentByIdQueryHandler : IQueryHandler<GetCommentByIdQuery, CommentDto>
    {
        private readonly CryptoNewsContext _context;
        private readonly IMapper _mapper;

        public GetCommentByIdQueryHandler(CryptoNewsContext context, IMapper map)
        {
            _context = context;
            _mapper = map;
        }

        public async Task<CommentDto> Handle(GetCommentByIdQuery query, CancellationToken token)
        {
            return
                _mapper.Map<CommentDto>(await _context.Comments
                .FirstOrDefaultAsync(c => c.Id.Equals(query.Id), token));
        }
    }
}
