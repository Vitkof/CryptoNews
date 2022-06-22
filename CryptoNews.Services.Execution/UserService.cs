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
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork,
                           IMapper mapper)
        {
            _unit = unitOfWork;
            _mapper = mapper;
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
                var roleId = _unit.Roles.Read(r => r.Name.Equals("User")).Id;
                await _unit.Users.CreateAsync(new User()
                {
                    Id = ud.Id,
                    Email = ud.Email,
                    PhoneNumber = ud.PhoneNumber,
                    PasswordHash = ud.PasswordHash,
                    RoleId = roleId,
                    RegisterTime = DateTime.Now,
                    FirstName = ud.FirstName,
                    LastName = ud.LastName,
                    Gender = ud.Gender,
                    ShortDescription = ud.ShortDescription
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

        #nullable enable
        public UserDto? GetUserByEmail(string email)
        {
            var user = _unit.Users.Read(user => user.Email.Equals(email));
            if (user is null) return null;
            return _mapper.Map<UserDto>(user);
        }

        public async Task<int> DeleteUser(UserDto ud)
        {
            _unit.Users.Delete(ud.Id);
            return await _unit.SaveChangesAsync();
        }

        public async Task<int> EditUser(UserDto ud)
        {
            return await Task.Run(async () =>
            {
                var user = _mapper.Map<User>(ud);

                _unit.Users.Update(user);
                return await _unit.SaveChangesAsync();
            });
        }

        public IEnumerable<UserDto> GetUsers()
        {
            var users = _unit.Users.ReadAll();
            return users.Select(u => _mapper.Map<UserDto>(u))
                        .ToList();
        }

        public UserDto GetUserById(Guid id)
        {
            return _mapper.Map<UserDto>(_unit.Users.ReadById(id));
        }

        public Task<string> GetEmailByRefreshToken(string token)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CheckAuthIsValid(UserDto ud)
        {
            throw new NotImplementedException();
        }

        public Task<UserDto> GetUserByRefreshToken(string token)
        {
            throw new NotImplementedException();
        }
    }
}
