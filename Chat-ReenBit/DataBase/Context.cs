using DataBase.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().HasData(
               new User() { UserId = 1, UserName = "FirstUser" },
               new User() { UserId = 2, UserName = "SecondUser" },
               new User() { UserId = 3, UserName = "ThirdUser" });

            modelBuilder.Entity<Chat>().HasData(
                         new Chat()
                         {
                             ChatId = 1,
                             ChatName = "TestGroupChat",
                             ChatType = ChatType.PersonalChat

                         },
                         new Chat()
                         {
                             ChatId = 2,
                             ChatType = ChatType.GroupChat,
                             ChatName = "PersonalChat"
                         });

            modelBuilder.Entity<Message>().HasData(
                new Message() { MessageId = 1, SendTime = DateTime.Now, ChatId = 1, UserName = "FirstUser", Text = "message from first user in personal chat", Visibility = Visibility.Everyone });

            modelBuilder.Entity<Chat>().HasMany(p => p.Users).WithMany(t => t.Chats).UsingEntity<Dictionary<string, object>>(
                "ChatUser",
                r => r.HasOne<User>().WithMany().HasForeignKey("UserId"),
                l => l.HasOne<Chat>().WithMany().HasForeignKey("ChatId"),
                je =>
                {
                    je.HasKey("ChatId", "UserId");
                    je.HasData(
                        new { ChatId = 1, UserId = 1 },
                        new { ChatId = 1, UserId = 2 });
                });

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            //optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-E3LF3J7\SQLEXPRESS;Initial Catalog=Chat;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            base.OnConfiguring(optionsBuilder);
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Message> Messages { get; set; }

    }
}
