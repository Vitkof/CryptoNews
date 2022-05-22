using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CryptoNews.Core.DTO;

namespace CryptoNews.Core.IServices
{
    public interface IUserService
    {
        string GetHashPassword(string pass);
        IEnumerable<UserDto> GetUsers();
        UserDto GetUserById(Guid id);
        UserDto GetUserByEmail(string email);

        Task<string> GetEmailByRefreshToken(string token);
        Task<bool> CheckAuthIsValid(UserDto ud);

        Task<bool> AddUser(UserDto ud);
        Task<int> EditUser(UserDto ud);
        Task<int> DeleteUser(UserDto ud);
    }
}
