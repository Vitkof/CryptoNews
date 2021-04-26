using CryptoNews.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoNews.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly CryptoNewsContext _context;

        public UserRepository(CryptoNewsContext context)
        {
            _context = context;
        }
        public async Task Create(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task CreateRange(IEnumerable<User> users)
        {
            await _context.Users.AddRangeAsync(users);
            await _context.SaveChangesAsync();
        }

        public IQueryable<User> ReadAllUsers()
        {
            return _context.Users.Where(user=>!String.IsNullOrWhiteSpace(user.FullName));
        }

        public User ReadById(Guid id)
        {
            return _context.Users.Find(id);
        }

        public User ReadByFullName(string fullName)
        {
            return _context.Users.Find(fullName);
        }

        public Task Update(User user)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
