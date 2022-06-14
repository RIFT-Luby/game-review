using GameReview.Application.ViewModels.Review;

namespace GameReview.Application.Interfaces
{
    public interface IReviewService
    {
        Task<ReviewResponse> CreateAsync(ReviewRequest model);
        Task<ReviewResponse> UpdateAsync(ReviewRequest model, int id);
        Task RemoveAsync(int id);
        Task<IEnumerable<ReviewResponse>> GetAllAsync();
        Task<ReviewResponse> GetByIdAsync(int id);
    }
}
