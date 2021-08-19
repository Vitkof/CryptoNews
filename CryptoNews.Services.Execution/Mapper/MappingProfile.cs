using AutoMapper;
using CryptoNews.Core.DTO;
using CryptoNews.DAL.Entities;

namespace CryptoNews.Services.Implement.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Comment, CommentDto>();
            CreateMap<CommentDto, Comment>();
            CreateMap<RssSource, RssSourceDto>();
            CreateMap<RssSourceDto, RssSource>();
        }
    }
}
