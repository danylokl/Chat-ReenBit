using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Models
{
    public enum Visibility{
        Everyone, EveryoneNoSender
    }
    public class Message
    {
        public int MessageId { get; set; }
        [Required]
        public string UserName { get; set; }
        public int ChatId { get; set; }
        public virtual Chat Chat { get; set; }
        public Visibility Visibility { get; set; }
        public DateTime SendTime { get; set; }
        [Required]
        public string Text { get; set; }
        
        public int? ReplyTo { get; set; }
    }
}
