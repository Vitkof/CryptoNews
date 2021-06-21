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

namespace CryptoNews.Services.Implement
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unit;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unit = unitOfWork;
        }

        public string GetHashPassword(string modelPass)
        {
            var sha256svc = new SHA256CryptoServiceProvider();
            var byteHash = sha256svc.ComputeHash(Encoding.UTF8.GetBytes(modelPass));
            return Encoding.UTF8.GetString(byteHash);
        }


        public async Task<bool> AddUser(UserDto ud)
        {
            try
            {
                await _unit.Users.Create(new User()
                {
                    Id = ud.Id,
                    Email = ud.Email,
                    PasswordHash = ud.PasswordHash
                });
                await _unit.SaveChangesAsync();
                return true;
            }
            catch(Exception ex)
            {
                Log.Error(ex.Message);
                return false;
            }
        }


        public Task<int> DeleteUser(UserDto ud)
        {
            throw new NotImplementedException();
        }

        public Task<int> EditUser(UserDto ud)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserDto>> FindUsers()
        {
            throw new NotImplementedException();
        }

        public UserDto GetUserById(Guid id)
        {
            throw new NotImplementedException();
        }

        
    }
}
