using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CryptoNews.Core.DTO;

namespace CryptoNews.Core.IServices
{
    public interface INewService
    {
        Task<NewDto> GetNewById(Guid id);
        Task<IEnumerable<NewDto>> GetNewBySourceId(Guid? id);
        Task<NewWithRssSourceNameDto> GetNewWithRssSourceNameById(Guid id);
    }
}
