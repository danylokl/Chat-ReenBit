using Chat_ReenBit.Models;

namespace Chat_ReenBit.ChatHub
{
    public interface IMessageHub
    {
        public Task Send(List<MessageClientDto> data);
    }
}
