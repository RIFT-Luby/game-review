namespace GameReview.Application.ViewModels.Review
{
    public class ReviewRequest
    {
        public int UserId { get; set; }
        public int GameId { get; set; }
        public string ReviewUser { get; set; }
        public int Score { get; set; }
    }
}
