using Chat_ReenBit.Identity;
using Chat_ReenBit.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Chat_ReenBit.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserIdentityController : ControllerBase
    {
        private UserManager<ApplicationUser> _userManager = null;
        private SignInManager<ApplicationUser> _signInManager = null;

        public UserIdentityController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;

        }

        [HttpPost]
        //[Route("/Login/{username}/{password}")]
        [Route("/Login")]
        public async Task Login([FromBody] LoginModel loginModel)
        {
            var result = await _signInManager.PasswordSignInAsync(loginModel.Username, loginModel.Password, false, false);
            return;
        }

        [HttpPost]
        [Route("/Logout")]
        public async Task<IActionResult> LogOut()
        {
            try
            {
                await _signInManager.SignOutAsync();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpGet]
        [Route("/Login/UserName")]
        public async Task<IActionResult> GetLoginedUserName()
        {
            try
            {
                var result = await _userManager.GetUserAsync(HttpContext.User);

                return Ok(result);
            }
            catch (Exception e)
            {

                return BadRequest(e);
            }


        }

        [HttpGet]
        [Route("/Login/[action]")]
        public async Task<IActionResult> CheckIfAuthenticated()
        {

            var result = (HttpContext.User != null) && HttpContext.User.Identity.IsAuthenticated;

            return Ok(result);
        }
    }
}
