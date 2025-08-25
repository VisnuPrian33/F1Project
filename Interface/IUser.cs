using F1Project.Models;

namespace F1Project.Interface
{
    public interface IUser
    {
        public Task<List<User>> GetUsersAsync();
        public Task<User> AddUserAsync(User user);
        public Task<List<User>> GetUsersByRoleAsync(string role);
    }
}
