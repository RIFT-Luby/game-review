
using GameReview.Domain.Models;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;

namespace GameReview.Application.Params
{
    public class GameParams
    {
        public string? Name { get; set; }
        public string? Developer { get; set; }
        public int? skip { get; set; }
        public int? take { get; set; } = 5;
        public int? ScoreMaiorQue { get; set; }
        public int? ScoreMenorQue { get; set; }
        public string? Console { get; set; }

        public Expression<Func<Game, bool>> Filter()
        {
            var predicate = PredicateBuilder.New<Game>();

            if (!string.IsNullOrEmpty(Name))
                predicate = predicate.And(n => EF.Functions.Like(n.Name, $"%{Name}%"));

            if (!string.IsNullOrEmpty(Developer))
                predicate = predicate.And(n => EF.Functions.Like(n.Developer, $"%{Developer}%"));

            if (ScoreMaiorQue.HasValue)
                predicate = predicate.And(x => x.Score >= ScoreMaiorQue);

            if (ScoreMenorQue.HasValue)
                predicate = predicate.And(x => x.Score <= ScoreMenorQue);

            if (!string.IsNullOrEmpty(Console))
                predicate = predicate.And(n => EF.Functions.Like(n.Console, $"%{Console}%"));

            return (predicate.IsStarted) ? predicate : null;
        }
    }
}
