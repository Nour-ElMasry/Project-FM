using Application.Abstract;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public async Task AddUser(User u)
        {
            await _context.Users.AddAsync(u);
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _context.Users
                .Include(u => u.UserPerson)
                .Take(100)
                .ToListAsync();
        }

        public async Task<User> GetUserById(long id)
        {
            return await _context.Users
                .Include(u => u.UserPerson)
                .SingleOrDefaultAsync(u => u.UserId == id);
        }

        public async Task<User> GetUserByName(string name)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Username == name);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUser(long id, User u)
        {
            var user = await GetUserById(id);

            if (user != null)
            {
                user.Username = u.Username;
                user.Password = u.Password;
            }
        }
    }
}
