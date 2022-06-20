using GameReview.Application.Interfaces;
using GameReview.Application.ViewModels.UserViews;
using Microsoft.AspNetCore.Mvc;

namespace GameReview.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:ApiVersion}/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateUserRequest model)
        {
            var result = await _userService.RegisterAsync(model);
            return Ok(result);
        }

        [HttpPut("img/{id:int}")]
        public async Task<ActionResult> PutImg([FromRoute] int id, [FromForm] IFormFile img)
        {
            await _userService.UploadImg(id, img);
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put([FromBody] UserRequest model, [FromRoute] int id)
        {
            var result = await _userService.UpdateAsync(model, id);
            return Ok(result);
        }

        [HttpPut("password/{id:int}")]
        public async Task<ActionResult> PutPassword([FromBody] PasswordRequest model, [FromRoute] int id)
        {
            var result = await _userService.UpdatePasswordAsync(model, id);
            return Ok(result);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            var result = await _userService.RemoveAsync(id);
            return Ok(result);
        }

        [HttpDelete("img/{id:int}")]
        public async Task<ActionResult> DeleteImg(int id)
        {
            var result = await _userService.RemoveImg(id);
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetByIdAsync(int id)
        {
            var result = await _userService.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpGet("img/{id:int}")]
        public ActionResult GetImgById(int id)
        {
            var result =  _userService.GetImg(id);
            return Ok(result);
        }



    }
}
