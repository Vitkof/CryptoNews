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
    public class GetCommentsByNewsIdQueryHandler : IQueryHandler<GetCommentsByNewsIdQuery, IEnumerable<CommentWithInfoDto>>
    {
        private readonly CryptoNewsContext _context;
        private readonly IMapper _mapper;

        public GetCommentsByNewsIdQueryHandler(CryptoNewsContext context, IMapper map)
        {
            _context = context;
            _mapper = map;
        }

        public async Task<IEnumerable<CommentWithInfoDto>> Handle(GetCommentsByNewsIdQuery query, CancellationToken token)
        {
            return
                await _context.Comments
                .Where(c => c.NewsId.Equals(query.NewsId))
                .Select(c => _mapper.Map<CommentWithInfoDto>(c))
                .ToListAsync(token);
        }
    }
}
