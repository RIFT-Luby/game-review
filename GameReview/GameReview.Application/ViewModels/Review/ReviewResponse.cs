using GameReview.Application.ViewModels.Game;
using GameReview.Application.ViewModels.UserViews;
using GameReview.Domain.Core;

namespace GameReview.Application.ViewModels.Review
{
    public class ReviewResponse : Register
    {
        public int GameId { get; set; }
        public GameResponse Game { get; set; }
        public int UserId { get; set; }
        public UserResponse User { get; set; }
        public string UserReview { get; set; }
        public int Score { get; set; }
    }
}
