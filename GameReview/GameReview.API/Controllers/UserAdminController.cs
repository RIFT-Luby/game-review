using Agenda.Application.ViewModels.Pagination;
using GameReview.Application.Constants;
using GameReview.Application.Interfaces;
using GameReview.Application.ViewModels.UserViews;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameReview.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:ApiVersion}/[controller]")]
    //[Authorize(Roles = Roles.Admin)]
    public class UserAdminController: ControllerBase
    {
        private readonly IUserService _userService;

        public UserAdminController(IUserService userService)
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
            var result = await _userService.UploadImg(id, img);
            return Ok(result);
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

        [HttpGet("reviews/{id:int}")]
        public async Task<ActionResult> GetWithReviews(int id)
        {
            var result = await _userService.GetByIdWithReviewsAsync(id);
            return Ok(result);
        }

        [HttpGet]
        public async Task<PaginationResponse<UserResponse>> GetAll([FromQuery] int? skip, int? take)
        {
            return new PaginationResponse<UserResponse>
            {
                Info = await _userService.GetAll(skip: skip, take: take),
                TotalPages = await _userService.CountAll(),
                Skip = skip,
                Take = take,
            };
        }

        [HttpGet("img/{id:int}")]
        public ActionResult GetImgById(int id)
        {
            var result = _userService.GetImg(id);
            return Ok(result);
        }
    }
}
