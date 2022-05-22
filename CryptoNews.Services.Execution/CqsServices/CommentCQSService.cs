using CryptoNews.Core.DTO;
using CryptoNews.Core.IServices;
using CryptoNews.DAL.CQS;
using CryptoNews.DAL.CQS.Commands;
using CryptoNews.DAL.CQS.Queries.Comment;
using MediatR;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CryptoNews.Services.Implement.CqsServices
{
    public class CommentCQSService : ICommentService
    {
        private readonly IMediator _mediator;
        private readonly IQueryDispatcher _queryDispatcher;

        public CommentCQSService(IMediator mediator,
                                 IQueryDispatcher queryDispatcher)
        {
            _mediator = mediator;
            _queryDispatcher = queryDispatcher;
        }

        #region Commands
        public async Task AddComment(CommentDto cd)
        {
            try
            {
                await _mediator.Send(new AddCommentCommand() 
                { Comment = cd });
            }
            catch(Exception ex)
            {
                Log.Error($"Error AddComment: {ex.Message} {cd.Id}");
            }
        }

        public async Task<int> DeleteComment(CommentDto cd)
        {
            try
            {
                await _mediator.Send(new DeleteCommentCommand() 
                { Id = cd.Id });
                return 1;
            }
            catch(Exception ex)
            {
                Log.Error($"Error DeleteComment: {ex.Message} {cd.Id}");
                return 0;
            }
        }

        public async Task<int> EditComment(CommentDto cd)
        {
            try
            {
                var commentDto = await GetCommentByIdAsync(cd.Id);
                commentDto.Text = cd.Text;
                await _mediator.Send(new EditCommentCommand() 
                { Comment = commentDto });
                return 1;
            }
            catch(Exception ex)
            {
                Log.Error($"Error EditComment:  {ex.Message} {cd.Id}");
                return 0;
            }
        }
        #endregion

        #region Queries
        public async Task<CommentDto> GetCommentByIdAsync(Guid commentId)
        {
            try
            {
                var query = new GetCommentByIdQuery() { Id = commentId };
                return
                    await _queryDispatcher
                    .HandleAsync<GetCommentByIdQuery, CommentDto>(query, new CancellationToken());
            }
            catch (Exception ex)
            {
                Log.Error($"Error GetCommentById: {ex.Message} {commentId}");
                return null;
            }
        }

        public IEnumerable<CommentWithInfoDto> GetCommentsByNewsId(Guid newsId)
        {
            try
            {
                var query = new GetCommentsByNewsIdQuery() { NewsId = newsId };
                var comments = _queryDispatcher
                    .Dispatch<GetCommentsByNewsIdQuery, IEnumerable<CommentWithInfoDto>>(query, new CancellationToken());
                return comments;
            }
            catch(Exception ex)
            {
                Log.Error($"Error GetCommentsByNewsId {ex.Message} {newsId}");
                return null;
            }
        }

        public CommentDto GetCommentById(Guid id)
        {
            return GetCommentByIdAsync(id).Result;
        }
        #endregion
    }
}
