using DataBase.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Chat_ReenBit.Models
{
   
    public class MessageClientDto 
    {
        public int MessageId { get; set; }
        public string UserName { get; set; }
        public int ChatId { get; set; }
        public string Sendtime { get; set; }
        public string Text { get; set; }
        public Visibility Visibility { get; set; }
        public int? ReplyTo { get; set; }
        public static MessageClientDto ToDto(Message message)
        {
            return new MessageClientDto()
            {
                UserName = message.UserName,
                ChatId = message.ChatId,
                Sendtime = message.SendTime.ToString(),
                Text = message.Text,
                MessageId = message.MessageId,
                Visibility = message.Visibility,
                ReplyTo = message.ReplyTo
            };
        }
        public Message ToModel()
        {
            return new Message()
            {
                UserName = this.UserName,
                ChatId = this.ChatId,
                Text = this.Text,
                SendTime = DateTime.Parse(this.Sendtime),
                MessageId = this.MessageId,
                Visibility=this.Visibility, 
                ReplyTo=this.ReplyTo
             
            };
        }
    }
}
