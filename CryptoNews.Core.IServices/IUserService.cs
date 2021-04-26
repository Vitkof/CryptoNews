using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CryptoNews.Core.DTO;

namespace CryptoNews.Core.IServices
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> FindUsers();
        UserDto GetUserById(Guid id);

        Task AddUser(UserDto ud);
        Task AddRangeUsers(IEnumerable<UserDto> usersDto);
        Task<int> EditUser(UserDto ud);
        Task<int> DeleteUser(UserDto ud);
    }
}
