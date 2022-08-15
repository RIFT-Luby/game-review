using GameReview.Application.Params;
using GameReview.Application.ViewModels.Review;
using GameReview.Domain.Models;
using System.Linq.Expressions;

namespace GameReview.Application.Interfaces
{
    public interface IReviewAdminService
    {
        Task<ReviewResponse> RemoveAsync(int id);
        Task<IEnumerable<ReviewResponse>> GetAllAsync(Expression<Func<Review, bool>> expression = null, int? skip = null, int? take = null);
        Task<ReviewResponse> GetByIdAsync(int id);
        Task<IEnumerable<ReviewResponse>> GetParamsAsync(ReviewAdminParams reviewAdminParams);
        Task<int> CountAll(Expression<Func<ReviewRequest, bool>>? filter = null);
    }
}
