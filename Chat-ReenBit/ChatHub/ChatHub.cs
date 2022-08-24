using Chat_ReenBit.Models;
using Microsoft.AspNetCore.SignalR;

namespace Chat_ReenBit.ChatHub
{
    public class MessageHub : Hub<IMessageHub>
    {
        protected IHubContext<MessageHub> hubContext;

        public MessageHub(IHubContext<MessageHub> hubContext)
        {
            this.hubContext = hubContext;
        }

        public async Task Send(List<MessageClientDto> data)
        {
            await hubContext.Clients.All.SendAsync("messageData", data);

        }
    }
}
