using GameReview.Application.Constants;
using GameReview.Application.Interfaces;
using GameReview.Application.ViewModels;
using GameReview.Application.ViewModels.Email;
using GameReview.Application.ViewModels.UserViews;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GameReview.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:ApiVersion}/[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Post([FromBody] CreateUserRequest model)
        {
            model.UserRoleId = 2;
            var result = await _userService.RegisterAsync(model);
            return Ok(result);
        }

        [HttpPut("img")]
        public async Task<ActionResult> PutImg([FromForm] IFormFile img)
        {
            var id = GetUserId();
            var result = await _userService.UploadImg(id, img);
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] UserRequest model)
        {
            var id = GetUserId();
            model.UserRoleId = 2;
            var result = await _userService.UpdateAsync(model, id);
            return Ok(result);
        }

        [HttpPut("password")]
        public async Task<ActionResult> PutPassword([FromBody] PasswordRequest model)
        {
            var id = GetUserId();
            var result = await _userService.UpdatePasswordAsync(model, id);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete()
        {
            var id = GetUserId();
            var result = await _userService.RemoveAsync(id);
            return Ok(result);
        }

        [HttpDelete("img")]
        public async Task<ActionResult> DeleteImg()
        {
            var id = GetUserId();
            var result = await _userService.RemoveImg(id);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult> GetUser()
        {
            var id = GetUserId();
            var result = await _userService.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpGet("reviews")]
        public async Task<ActionResult> GetUserReview()
        {
            var id = GetUserId();
            var result = await _userService.GetByIdWithReviewsAsync(id);
            return Ok(result);
        }

        [HttpGet("img")]
        public ActionResult GetImgById()
        {
            var id = GetUserId();
            var result =  _userService.GetImg(id);
            return Ok(result);
        }

        private int GetUserId()
        {
            return int.Parse(User.FindFirst(ClaimTypes.Sid)!.Value);
        }

    }
}
