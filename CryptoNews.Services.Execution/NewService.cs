using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CryptoNews.Core.DTO;
using CryptoNews.Core.IServices;

namespace CryptoNews.Services.Execution
{
    public class NewService : INewService
    {
        public async Task<NewDto> GetNewById(Guid id)
        {
            throw new NotImplementedException();
        }
        public async Task<IEnumerable<NewDto>> GetNewBySourceId(Guid? id)
        {
            throw new NotImplementedException();
        }
        public async Task<NewWithRssSourceNameDto> GetNewWithRssSourceNameById(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
