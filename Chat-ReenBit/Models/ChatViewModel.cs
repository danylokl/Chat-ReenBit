﻿using Chat_ReenBit.Models;
using DataBase.Models;

namespace Test_Task_ReenBit.Models
{
    public class ChatViewModel
    { public int ChatId { get; set; }
        public string ChatName { get; set; }
       //public List<MessageClientDto> Messages { get; set; }
        public List<UserDto> Users { get; set; }
        public static ChatViewModel ToViewModel(Chat chat)
        {
            return new ChatViewModel() { ChatId=chat.ChatId,ChatName=chat.ChatName, Users=chat.Users.Select(p=>UserDto.ToDtoModel(p)).ToList()/*, Messages=chat.Messages.Select(p=> MessageClientDto.ToDto(p)).ToList()*/};

        }
    }
}
