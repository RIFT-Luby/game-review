using GameReview.Application.Constants;
using GameReview.Application.Interfaces;
using GameReview.Application.ViewModels.Review;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameReview.API.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:ApiVersion}/[controller]")]
    [ApiController]
    [Authorize(Roles = Roles.Common)]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] ReviewRequest model)
        {
            var created = await _reviewService.CreateAsync(model);
            return Ok(created);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateAsync([FromBody] ReviewRequest model, [FromRoute] int id)
        {
            var created = await _reviewService.UpdateAsync(model, id);
            return Ok(created);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> RemoveAsync([FromRoute] int id)
        {
            var review = await _reviewService.RemoveAsync(id);
            return Ok(review);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var reviews = await _reviewService.GetAllAsync();
            return Ok(reviews);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var review = await _reviewService.GetByIdAsync(id);
            return Ok(review);
        }
    }
}
