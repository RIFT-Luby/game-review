﻿namespace GameReview.Application.ViewModels.Review
{
    public class ReviewRequest
    {
        public int GameId { get; set; }
        public string UserReview { get; set; }
        public int Score { get; set; }
    }
}
