using DataBase.Models;

namespace Test_Task_ReenBit.Models
{
    public class ChatViewModel
    { public int ChatId { get; set; }
       public List<MessageDto> Messages { get; set; }
        
        public static ChatViewModel ToViewModel(Chat chat)
        {
            return new ChatViewModel() { ChatId=chat.ChatId, Messages=chat.Messages.Select(p=>MessageDto.ToDtoModel(p)).ToList()};

        }
    }
}
