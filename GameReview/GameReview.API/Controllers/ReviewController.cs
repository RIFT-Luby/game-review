using Agenda.Application.ViewModels.Pagination;
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
    [Authorize]
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
        public async Task<PaginationResponse<ReviewResponse>> GetAllAsync([FromQuery] int? skip, int? take = 5)
        {
            return new PaginationResponse<ReviewResponse>
            {
                Info = await _reviewService.GetAllAsync(skip: skip, take: take),
                TotalPages = await _reviewService.CountAll(),
                Skip = skip,
                Take = take,
            };
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var review = await _reviewService.GetByIdAsync(id);
            return Ok(review);
        }

        [HttpGet("GetMyReviews")]
        public async Task<IActionResult> GetMyReviewsAsync([FromQuery] int? skip, [FromQuery] int? take)
        {
            var reviews = await _reviewService.GetMyReviewsAsync(skip: skip, take: take);
            return Ok(reviews);
        }
    }
}
