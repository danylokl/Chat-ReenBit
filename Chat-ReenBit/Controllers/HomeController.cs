using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Test_Task_ReenBit.Models;
using Test_Task_ReenBit.Services;

namespace Test_Task_ReenBit.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserService _userService;
        private readonly ChatService _chatService;
        private readonly MessageService _messageService;
        public HomeController(ILogger<HomeController> logger, UserService userService, ChatService chatService, MessageService messageService)
        {
            _logger = logger;
            _userService = userService;
            _chatService = chatService;
            _messageService = messageService;
        }
        [HttpGet]
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
        public async Task<IActionResult> ButtonClick(int id)
        {
            var message = await _messageService.GetMessageAsync(id);
            message.Text = "dfg";
        await  _messageService.UpdateMessage(message);
            return RedirectToAction("Chat",new { id = message.ChatId });
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}