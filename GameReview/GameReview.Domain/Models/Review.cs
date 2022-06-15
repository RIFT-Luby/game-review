using GameReview.Domain.Core;

namespace GameReview.Domain.Models
{
    public class Review : Register
    {
        public int GameId { get; set; }
        public Game Game { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string ReviewUser { get; set; }
        public int Score { get; set; }
    }
}
