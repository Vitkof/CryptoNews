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

        public Task<int> DeleteUser(UserDto ud)
        {
            throw new NotImplementedException();
        }

        public Task<int> EditUser(UserDto ud)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<UserDto>> FindUsers()
        {
            try
            {
                var query = new GetAllUsersQuery();
                var users = await _queryDispatcher
                    .HandleAsync<GetAllUsersQuery, IEnumerable<UserDto>>(query, new CancellationToken());
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
    }
}
