using DataBase.Models;

namespace Chat_ReenBit.Services
{
    public class MessageService
    {
        private readonly IRepository<Chat> _chatRepository;
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Message> _messageRepository;

        public MessageService(IRepository<Chat> chatRepository, IRepository<User> userRepository, IRepository<Message> messageRepository)
        {
            _chatRepository = chatRepository;
            _userRepository = userRepository;
            _messageRepository = messageRepository;
        }

        public async Task<List<Message>> GetAllMessagesAsync()
        {
            var result = await _messageRepository.GetAllAsync();
            return result;
        }
        public async Task<Message> GetMessageAsync(int id)
        {
            var result = await _messageRepository.GetByIdAsync(id);
            return result;
        }
        public async Task DeleteMessage(int id)
        {
            await _messageRepository.Remove(id);
        }
        public async Task CreateMessage(Message message)
        {
await _messageRepository.Create(message);
        }
        public async Task UpdateMessage(Message message)
        {
            await _messageRepository.Update(message);
        }
        public async Task UpdateMessageText(Message data)
        {
            var message= await _messageRepository.GetByIdAsync(data.MessageId);
            message.Text = data.Text;
            await _messageRepository.SaveChanges();
        }
        public async Task DeleteForSender(Message data)
        {
            var message = await _messageRepository.GetByIdAsync(data.MessageId);
            message.Visibility = Visibility.EveryoneNoSender;
            await _messageRepository.SaveChanges();
        }
        public async Task SaveChanges()
        {
            await _messageRepository.SaveChanges();
        }
    }

}
