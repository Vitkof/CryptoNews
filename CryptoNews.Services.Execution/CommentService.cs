using CryptoNews.Core.DTO;
using CryptoNews.Core.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CryptoNews.DAL.Repositories;
using CryptoNews.DAL.Entities;
using CryptoNews.DAL.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using Serilog;
using AutoMapper;

namespace CryptoNews.Services.Implement
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;

        public CommentService(IUnitOfWork unitOfWork,
            IMapper map)
        {
            _unit = unitOfWork;
            _mapper = map;
        }

        public IEnumerable<CommentDto> GetCommentsByNewsId(Guid newsId)
        {
            return _unit.Comments
                .ReadMany(comm => comm.NewsId.Equals(newsId))
                .OrderBy(comm => comm.Rating)
                .ThenBy(comm => comm.CreateAt)
                .Select(comm => _mapper.Map<CommentDto>(comm)).ToList();
        }

        public async Task AddComment(CommentDto cd)
        {
            await _unit.Comments.Create(_mapper.Map<Comment>(cd));
            await _unit.SaveChangesAsync();
        }

        public async Task<int> DeleteComment(CommentDto cd)
        {
            await _unit.Comments.Delete(cd.Id);
            return await _unit.SaveChangesAsync();
        }

        public async Task<int> EditComment(CommentDto cd)
        {
            await _unit.Comments.Update(_mapper.Map<Comment>(cd));
            return await _unit.SaveChangesAsync();
        }
    }
}
