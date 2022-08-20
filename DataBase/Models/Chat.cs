using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Models
{
    public enum ChatType
    {
        PersonalChat, GroupChat
    };
    public class Chat
    {
        public int ChatId { get; set; }
        public ChatType ChatType { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Message> Messages { get; set; }

    }
}
