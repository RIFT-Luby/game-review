using Agenda.Application.ViewModels.Pagination;
using GameReview.Application.Constants;
using GameReview.Application.Interfaces;
using GameReview.Application.Params;
using GameReview.Application.ViewModels.Review;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameReview.API.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:ApiVersion}/[controller]")]
    [ApiController]
    [Authorize(Roles = Roles.Admin)]
    public class ReviewAdminController : ControllerBase
    {
        private readonly IReviewAdminService _reviewAdminService;

        public ReviewAdminController(IReviewAdminService reviewAdminService)
        {
            _reviewAdminService = reviewAdminService;
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> RemoveAsync([FromRoute] int id)
        {
            var review = await _reviewAdminService.RemoveAsync(id);
            return Ok(review);
        }

        [HttpGet]
        public async Task<PaginationResponse<ReviewResponse>> GetAllAsync([FromQuery] ReviewAdminParams query)
        {
            return new PaginationResponse<ReviewResponse>
            {
                Info = await _reviewAdminService.GetAllAsync(query.Filter(), skip: query.skip, take: query.take),
                TotalPages = await _reviewAdminService.CountAll(),
                Skip = query.skip,
                Take = query.take,
            };
        }


        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var review = await _reviewAdminService.GetByIdAsync(id);
            return Ok(review);
        }

        [HttpGet("Filtros")]
        [AllowAnonymous]
        public async Task<IActionResult> GetParamsAsync([FromQuery] ReviewAdminParams reviewAdminParams)
        {
            var reviews = await _reviewAdminService.GetParamsAsync(reviewAdminParams);
            return Ok(reviews);
        }
    }
}
