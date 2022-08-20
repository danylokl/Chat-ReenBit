using DataBase.Models;

namespace Test_Task_ReenBit.Services
{
    public class UserService
    {
        private readonly IRepository<Chat> _chatRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Message> _messageRepository;

        public UserService(IRepository<Chat> chatRepository, IRepository<User> userRepository, IRepository<Message> messageRepository)
        {
            _chatRepository = chatRepository;
            _userRepository = userRepository;
            _messageRepository = messageRepository;
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            var result = await _userRepository.GetAllAsync();
            return result;
        }
        public async Task<User> GetUserAsync(int id)
        {
            var result = await _userRepository.GetByIdAsync(id);
            return result;
        }
        public async Task DeleteUserk(int id)
        {
            await _userRepository.Remove(id);
        }
        public async Task CreateUser(User user)
        {
            await _userRepository.Create(user);
        }
        public async Task UpdateUser(User user)
        {
            await _userRepository.Update(user);
        }
        public async Task SaveChanges()
        {
            await _userRepository.SaveChanges();
        }
    }
}
