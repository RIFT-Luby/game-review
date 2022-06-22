using GameReview.Domain.Models;
using LinqKit;
using System.Linq.Expressions;

namespace GameReview.Application.Params
{
    public class ReviewAdminParams : BaseParams<Review>
    {
        public int? UserId { get; set; }
        public int? GameId { get; set; }
        public int? ScoreMaiorQue { get; set; }
        public int? ScoreMenorQue { get; set; }
        public string? DataCriacaoMaiorQue { get; set; }
        public string? DataCriacaoMenorQue { get; set; }

        public override Expression<Func<Review, bool>> Filter()
        {
            var predicate = PredicateBuilder.New<Review>();

            if (UserId.HasValue)
                predicate = predicate.And(x => x.UserId == UserId);

            if (GameId.HasValue)
                predicate = predicate.And(x => x.GameId == GameId);

            if (ScoreMaiorQue.HasValue)
                predicate = predicate.And(x => x.Score >= ScoreMaiorQue);

            if (ScoreMenorQue.HasValue)
                predicate = predicate.And(x => x.Score <= ScoreMenorQue);

            if (!string.IsNullOrEmpty(DataCriacaoMaiorQue))
                predicate = predicate.And(x => x.CreatedAt >= DateTime.Parse(DataCriacaoMaiorQue));

            if (!string.IsNullOrEmpty(DataCriacaoMenorQue))
                predicate = predicate.And(x => x.CreatedAt <= DateTime.Parse(DataCriacaoMenorQue));

            return predicate;
        }
    }
}
