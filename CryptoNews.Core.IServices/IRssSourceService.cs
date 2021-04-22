using CryptoNews.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoNews.Core.IServices
{
    public interface IRssSourceService
    {
        Task<NewDto> GetNewById(Guid id);
        Task<IEnumerable<NewDto>> GetNewBySourceId(Guid? id);
        Task<NewWithRssSourceNameDto> GetNewWithRssSourceNameById(Guid id);

        Task<NewDto> AddNew(Guid id);
        Task<IEnumerable<NewDto>> AddRangeNews(IEnumerable<NewDto> news);
        Task<int> EditNew(NewDto news);
        Task<int> DeleteNew(NewDto news);
    }
}
