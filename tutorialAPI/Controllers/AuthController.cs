using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using tutorialAPI.DTO;
using tutorialAPI.Services;

namespace tutorialAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authService;
        public AuthController(IAuthenticationService authenticationService)
        {
            _authService = authenticationService;
        }

        [HttpPost]
        public ActionResult Login(UserDTO userDto)
        {
            var user = _authService.CheckUser(userDto);
            if (user == null)
            {
                return Unauthorized();
            }
            var token = _authService.GenerateJWT(user);
            return Ok(new { accessToken = token });
        }
    }
}
