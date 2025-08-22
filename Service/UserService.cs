using F1Project.Interface;
using F1Project.Models;

namespace F1Project.Service
{
    public class UserService
    {
        private readonly IUser _userRepository;
        public UserService(IUser userRepository)
        {
            _userRepository = userRepository;
        }
        
        public Task<List<User>> GetUsersAsync() => _userRepository.GetUsersAsync();
        public Task<User?> AddUserAsync(User user) => _userRepository.AddUserAsync(user);
    }
}
