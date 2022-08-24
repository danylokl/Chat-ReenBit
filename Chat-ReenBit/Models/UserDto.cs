using DataBase.Models;

namespace Chat_ReenBit.Models
{
    public class UserDto
    {
        public int Userid { get; set; }
        public string Username { get; set; }
        public static UserDto ToDtoModel(User user)
        {

            return new UserDto() { Userid = user.UserId, Username = user.UserName };
        }
    }

}
