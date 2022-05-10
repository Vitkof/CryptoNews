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

        public IEnumerable<CommentWithInfoDto> GetCommentsByNewsId(Guid newsId)
        {
            var comments = _unit.Comments
                .ReadMany(comm => comm.NewsId.Equals(newsId))
                .OrderBy(comm => comm.Rating)
                .ThenBy(comm => comm.CreateAt)
                .Select(comm => _mapper.Map<CommentWithInfoDto>(comm));

            foreach (var comm in comments) 
            {
                var user = _unit.Users.ReadById(comm.UserId);
                comm.UserFullName = user.FullName;
                comm.UserAvatarUrl = user.AvatarUrl;
            }

            return comments.ToList();
        }

        public async Task AddComment(CommentDto cd)
        {
            await _unit.Comments.CreateAsync(_mapper.Map<Comment>(cd));
            await _unit.SaveChangesAsync();
        }

        public async Task<int> DeleteComment(CommentDto cd)
        {
            _unit.Comments.Delete(cd.Id);
            return await _unit.SaveChangesAsync();
        }

        public async Task<int> EditComment(CommentDto cd)
        {
            _unit.Comments.Update(_mapper.Map<Comment>(cd));
            return await _unit.SaveChangesAsync();
        }
    }
}
