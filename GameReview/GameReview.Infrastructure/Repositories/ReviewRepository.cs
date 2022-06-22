using GameReview.Domain.Interfaces.Repositories;
using GameReview.Domain.Models;
using GameReview.Infrastructure.Context;

namespace GameReview.Infrastructure.Repositories
{
    public class ReviewRepository : BaseRepository<Review>, IReviewRepository
    {
        public ReviewRepository(ApplicationContext context) : base(context)
        {
            
        }
    }
}
