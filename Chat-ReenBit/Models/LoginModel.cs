using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Chat_ReenBit.Models
{
 
    public class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
