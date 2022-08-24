using DataBase.Models;

namespace Chat_ReenBit.Services
{
    public class ChatService
    {
        private readonly IRepository<Chat> _chatRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Message> _messageRepository;

        public ChatService(IRepository<Chat> chatRepository, IRepository<User> userRepository, IRepository<Message> messageRepository)
        {
            _chatRepository = chatRepository;
            _userRepository = userRepository;
            _messageRepository = messageRepository;
        }

        public async Task<List<Chat>> GetAllChatsAsync()
        {
            var result = await _chatRepository.GetAllAsync();
            return result;
        }
        public async Task<Chat> GetChatAsync(int id)
        {
            var result = await _chatRepository.GetByIdAsync(id);
            return result;
        }
        public async Task DeleteChat(int id)
        {
            await _chatRepository.Remove(id);
        }
        public async Task CreateChat(Chat chat)
        {
            await _chatRepository.Create(chat);
        }
        public async Task UpdateChat(Chat chat)
        {
            await _chatRepository.Update(chat);
        }
        public async Task SaveChanges()
        {
            await _chatRepository.SaveChanges();
        }
    }
}

