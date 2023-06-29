using Google.Apis.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MultipleTypesAuthentication.DTO;
using MultipleTypesAuthentication.Helpers;
using MultipleTypesAuthentication.Services.Interfaces.UserInterface;

namespace MultipleTypesAuthentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OAuthController : ControllerBase
    {
        private readonly IUserService _userService;
        public OAuthController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("OAuthGoogleLogin")]  
        public async Task<IActionResult> OAuthGoogleLogin([FromBody] UserTokenGoogle userToken)
        {
            return Ok(_userService.ValidateTokenGoogle(userToken));
        }
    }
}
