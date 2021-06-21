using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CryptoNews.Core.DTO;

namespace CryptoNews.Core.IServices
{
    public interface IUserService
    {
        string GetHashPassword(string pass);
        Task<IEnumerable<UserDto>> FindUsers();
        UserDto GetUserById(Guid id);

        Task<bool> AddUser(UserDto ud);
        Task<int> EditUser(UserDto ud);
        Task<int> DeleteUser(UserDto ud);
    }
}
