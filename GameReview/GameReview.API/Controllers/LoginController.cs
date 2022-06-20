using GameReview.Application.Interfaces;
using GameReview.Application.ViewModels.Login;
using Microsoft.AspNetCore.Mvc;

namespace GameReview.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:ApiVersion}/[controller]")]
    public class LoginController : ControllerBase
    {
        private ILoginService _loginService;
        private IUserService _userService;
        private ITokenGeneratorService _tokenGeneratorService;

        public LoginController(ILoginService loginService,
                               ITokenGeneratorService tokenGeneratorService, IUserService userService)
        {
            _loginService = loginService;
            _tokenGeneratorService = tokenGeneratorService;
            _userService = userService;
        }

        [HttpPost]
        public async Task<ActionResult> LoginRequest([FromBody] LoginRequest login)
        {
            var result = await _loginService.Login(login);
            var token = _tokenGeneratorService.GenerateToken(result);

            return Ok(new
            {
                Token = token
            });
        }

        [HttpPost("recoverPassword")]
        public async Task<IActionResult> SendMail([FromQuery] string userName)
        {
            await _userService.RecoverPassword(userName);
            return Ok();

        }

    }
}
