using GameReview.Domain.Models;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GameReview.Application.Params
{
    public class ReviewAdminParams : BaseParams<Review>
    {
        public string? UserName { get; set; }
        public string? GameName { get; set; }
        public int? ScoreMaiorQue { get; set; }
        public int? ScoreMenorQue { get; set; }
        public string? DataCriacaoMaiorQue { get; set; }
        public string? DataCriacaoMenorQue { get; set; }
        public int? skip { get; set; }
        public int? take { get; set; } = 5;

        public override Expression<Func<Review, bool>> Filter()
        {
            var predicate = PredicateBuilder.New<Review>();

            if (!string.IsNullOrEmpty(UserName))
                predicate = predicate.And(x => EF.Functions.Like(x.User.UserName, $"%{UserName}%"));

            if (!string.IsNullOrEmpty(GameName))
                predicate = predicate.And(x => EF.Functions.Like(x.Game.Name, $"%{GameName}%"));

            if (ScoreMaiorQue.HasValue)
                predicate = predicate.And(x => x.Score >= ScoreMaiorQue);

            if (ScoreMenorQue.HasValue)
                predicate = predicate.And(x => x.Score <= ScoreMenorQue);

            if (!string.IsNullOrEmpty(DataCriacaoMaiorQue))
                predicate = predicate.And(x => x.CreatedAt >= DateTime.Parse(DataCriacaoMaiorQue));

            if (!string.IsNullOrEmpty(DataCriacaoMenorQue))
                predicate = predicate.And(x => x.CreatedAt <= DateTime.Parse(DataCriacaoMenorQue));

            return predicate.IsStarted ? predicate : null;
        }
    }
}
