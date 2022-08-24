using Chat_ReenBit.ChatHub;
using Chat_ReenBit.Identity;
using Chat_ReenBit.Models;
using Chat_ReenBit.Services;
using DataBase.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Diagnostics;
using Test_Task_ReenBit.Models;

namespace Chat_ReenBit.Controllers
{
    public class ChatController : Controller
    {
        private UserManager<ApplicationUser> _userManager = null;
        private readonly ILogger<ChatController> _logger;
        private readonly UserService _userService;
        private readonly ChatService _chatService;
        private readonly MessageService _messageService;
        private readonly MessageHub _messageHub;
        public ChatController(UserManager<ApplicationUser> userManager, ILogger<ChatController> logger, UserService userService, ChatService chatService, MessageService messageService, MessageHub messageHub)
        {
            _userManager = userManager;
            _logger = logger;
            _userService = userService;
            _chatService = chatService;
            _messageService = messageService;
            _messageHub = messageHub;
        }

        [HttpGet]
        [Authorize]

        [Route("[action]")]
        public async Task<IActionResult> Index()
        {
            var chats = await _chatService.GetAllChatsAsync();
            var model = chats.Select(p => ChatViewModel.ToViewModel(p)).ToList();
            return Ok(model);
        }
        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<IActionResult> Chat(int id)
        {
            var chat = await _chatService.GetChatAsync(id);
            var model = ChatViewModel.ToViewModel(chat);
            return Ok(model);
        }
        [HttpGet]
        [Route("chatHub/{id}/{pagenumber}")]
        public async Task<IActionResult> GetMessages([FromRoute] int id, [FromRoute] int pagenumber)
        {
            pagenumber *= 20;
            try
            {
            var chat = await _chatService.GetChatAsync(id);
            ////var messages=chat.Messages.Select(a => a).ToList();
            //    var messages = chat.Messages.Or;
                var model=chat.Messages.OrderByDescending(p=>p.SendTime).Take(pagenumber).Select(p=> MessageClientDto.ToDto(p)).ToList();
                return Ok(model);
            }
            catch(NullReferenceException ex)
            {
                return BadRequest(ex);
            }
           
        }

        [HttpGet]
        [Route("[action]/{id}")]
        public async Task<IActionResult> GetMessageById(int id)
        {
            var result = await _messageService.GetMessageAsync(id);
            var model= MessageClientDto.ToDto(result);
            return Ok(model);
        }


        [HttpPost]
        [Authorize]
        [Route("[action]")]
        public async Task SendMessage([FromBody] MessageClientDto messageDto)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var message = new Message()
            {
                ChatId = messageDto.ChatId,
                UserName = user.UserName,
                Text = messageDto.Text,
                SendTime = DateTime.Now,
                Visibility = Visibility.Everyone,
                ReplyTo = messageDto.ReplyTo,
            };

            await _messageService.CreateMessage(message);
            var chat = await _chatService.GetChatAsync(messageDto.ChatId);
        chat.Messages.Add(message);
            await _messageHub.Send(chat.Messages.Select(p => MessageClientDto.ToDto(p)).ToList());
            await _chatService.SaveChanges();
        }
        [HttpPut]
        [Route("[action]")]
        public async Task EditMessage([FromBody] MessageClientDto messageDto)
        {
            var message = messageDto.ToModel();
            await _messageService.UpdateMessageText(message);
            var chat = await _chatService.GetChatAsync(messageDto.ChatId);
            await _messageHub.Send(chat.Messages.Select(p => MessageClientDto.ToDto(p)).ToList());

        }

        [HttpDelete]
        [Route("[action]")]
        public async Task DeleteMessage([FromBody] MessageClientDto messageDto)
        {
            await _messageService.DeleteMessage(messageDto.MessageId);

            var chat = await _chatService.GetChatAsync(messageDto.ChatId);
            await _messageHub.Send(chat.Messages.Select(p => MessageClientDto.ToDto(p)).ToList());
        }
        
        [HttpDelete]
        [Route("[action]")]
        public async Task DeleteMessageForSender([FromBody] MessageClientDto messageDto)
        {
            var message = messageDto.ToModel();
            await _messageService.DeleteForSender(message); 

            var chat = await _chatService.GetChatAsync(messageDto.ChatId);
            await _messageHub.Send(chat.Messages.Select(p => MessageClientDto.ToDto(p)).ToList());
        }


    }
}