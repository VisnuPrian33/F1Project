using F1Project.Interface;
using F1Project.Models;
using Microsoft.EntityFrameworkCore;

namespace F1Project.Repository
{
    public class UserRepository : IUser
    {
        private readonly F1projectContext _context;
        public UserRepository(F1projectContext context)
        {
            _context = context;
        }

        public async Task<User?> AddUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }
    }
}
