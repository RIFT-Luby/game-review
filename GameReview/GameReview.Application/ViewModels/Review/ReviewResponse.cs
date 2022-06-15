using GameReview.Domain.Core;

namespace GameReview.Application.ViewModels.Review
{
    public class ReviewResponse : Register
    {
        public GameResponse Game { get; set; }
        public UserResponse User { get; set; }
        public string ReviewUser { get; set; }
        public int Score { get; set; }
    }
}
