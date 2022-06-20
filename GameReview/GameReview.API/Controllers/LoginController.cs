using GameReview.Application.Interfaces;
using GameReview.Application.ViewModels.Login;
using Microsoft.AspNetCore.Mvc;

namespace GameReview.API.Controllers
{
    [ApiController]
    [Route("api/v1/login")]
    public class LoginController : ControllerBase
    {
        private ILoginService _loginService;
        private ITokenGeneratorService _tokenGeneratorService;

        public LoginController(ILoginService loginService,
                               ITokenGeneratorService tokenGeneratorService)
        {
            _loginService = loginService;
            _tokenGeneratorService = tokenGeneratorService;
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
    }
}
