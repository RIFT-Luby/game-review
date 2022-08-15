using GameReview.Application.ViewModels.Review;
using GameReview.Domain.Models;
using System.Linq.Expressions;

namespace GameReview.Application.Interfaces
{
    public interface IReviewService
    {
        Task<ReviewResponse> CreateAsync(ReviewRequest model);
        Task<ReviewResponse> UpdateAsync(ReviewRequest model, int id);
        Task<ReviewResponse> RemoveAsync(int id);
        Task<IEnumerable<ReviewResponse>> GetAllAsync(Expression<Func<Review, bool>> expression = null, int? skip = null, int? take = null);
        Task<ReviewResponse> GetByIdAsync(int id);
        Task<int> CountAll(Expression<Func<Review, bool>>? filter = null);
    }
}
