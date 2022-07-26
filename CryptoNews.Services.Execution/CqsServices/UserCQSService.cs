using CryptoNews.Core.DTO;
using CryptoNews.Core.IServices;
using CryptoNews.DAL.CQS;
using CryptoNews.DAL.CQS.Commands;
using CryptoNews.DAL.CQS.Queries.User;
using MediatR;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CryptoNews.Services.Implement.CqsServices
{
    public class UserCQSService : IUserService
    {
        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;
        private readonly IQueryDispatcher _queryDispatcher;

        public UserCQSService(IMediator mediator,
                              IConfiguration config,
                              IQueryDispatcher queryDispatcher)
        {
            _mediator = mediator;
            _configuration = config;
            _queryDispatcher = queryDispatcher;
        }


        public async Task<bool> AddUser(UserDto ud)
        {
            try
            {
                await _mediator.Send(new RegisterUserCommand()
                {
                    User = ud
                });
                return true;
            }
            catch(Exception ex)
            {
                Log.Error(ex, "Register wasn't successful");
                return false;
            }
        }


        public IEnumerable<UserDto> GetUsers()
        {
            try
            {
                var query = new GetAllUsersQuery();
                var users = _queryDispatcher
                    .Dispatch<GetAllUsersQuery, IEnumerable<UserDto>>(query, new CancellationToken());
                return users;
            }
            catch(Exception ex)
            {
                Log.Error($"Error GetAllUser: {ex.Message}");
                return null;
            }
        }

        public string GetHashPassword(string pass)
        {
            var specialValue = _configuration["Password:SecuritySymmetricKey"];
            var sha256 = new SHA256CryptoServiceProvider();
            var sha256data = sha256.ComputeHash(Encoding.UTF8.GetBytes(pass));
            var hashedPassword = Encoding.UTF8.GetString(sha256data);
            return hashedPassword;
        }

        public UserDto GetUserById(Guid id)
        {
            try
            {
                var query = new GetUserByIdQuery() { Id = id };
                var user = _queryDispatcher.Dispatch<GetUserByIdQuery, UserDto>(query, new CancellationToken());
                return user;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                throw;
            }
        }

        public UserDto GetUserByEmail(string email)
        {
            try
            {
                var query = new GetUserByEmailQuery() { Email = email };
                var user = _queryDispatcher
                    .Dispatch<GetUserByEmailQuery, UserDto>(query, new CancellationToken());
                return user;
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                throw;
            }
        }

        public async Task<string> GetEmailByRefreshToken(string token)
        {
            try
            {
                var query = new GetEmailByRefreshTokenQuery()
                { Token = token };
                var email = await _queryDispatcher
                    .HandleAsync<GetEmailByRefreshTokenQuery, string>(query, new CancellationToken());
                return email;
            }
            catch (Exception ex)
            {
                Log.Error($"Error GetEmailByRefreshToken: {ex.Message}");
                return null;
            }
        }

        public async Task<UserDto> GetUserByRefreshToken(string token)
        {
            try
            {
                var query = new GetUserByRefreshTokenQuery()
                { Token = token };
                var user = await _queryDispatcher
                    .HandleAsync<GetUserByRefreshTokenQuery, UserDto>(query, new CancellationToken());
                return user;
            }
            catch (Exception ex)
            {
                Log.Error($"Error GetUserByRefreshToken: {ex.Message}");
                return null;
            }
        }

        public async Task<int> EditUser(UserDto ud)
        {
            try
            {
                var userDto = GetUserById(ud.Id);
                userDto = ud;
                await _mediator.Send(new EditUserCommand()
                { User = userDto });
                return 1;
            }
            catch (Exception ex)
            {
                Log.Error($"Error UpdateUser:  {ex.Message} {ud.Id}");
                return 0;
            }
        }

        public async Task<int> DeleteUser(UserDto ud)
        {
            try
            {
                await _mediator.Send(new DeleteUserCommand()
                { Id = ud.Id });
                return 1;
            }
            catch (Exception ex)
            {
                Log.Error($"Error DeleteUser: {ex.Message} {ud.Id}");
                return 0;
            }
        }

        public async Task<bool> CheckAuthIsValid(UserDto ud)
        {
            try
            {
                var userValid = await _mediator.Send(new CheckAuthenticationQuery()
                {
                    Email = ud.Email,
                    PasswordHash = ud.PasswordHash
                });
                return userValid;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "CheckAuthentication was not successful");
                return false;
            }
        }
    }
}
