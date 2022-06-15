using GameReview.Domain.Interfaces.Repositories;
using GameReview.Domain.Models;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GameReview.Infrastructure.Repositories
{
    public class TempReviewRepository : IReviewRepository
    {
        Review review = new Review()
        {
            Id = 1,
            GameId = 1,
            UserId = 1,
            ReviewUser = "teste",
            Score = 1,
        };
        public Task DeleteAsync(Review model)
        {
            return Task.CompletedTask;
        }

        public Task<Review> FirstAsync(Expression<Func<Review, bool>> filter, Func<IQueryable<Review>, IIncludableQueryable<Review, object>> include = null)
        {
            return Task.FromResult(review);
        }

        public Task<Review> FirstAsyncAsTracking(Expression<Func<Review, bool>> filter, Func<IQueryable<Review>, IIncludableQueryable<Review, object>> include = null)
        {
            return Task.FromResult(review);
        }

        public Task<IEnumerable<Review>> GetDataAsync(Expression<Func<Review, bool>> filter = null, Func<IQueryable<Review>, IIncludableQueryable<Review, object>> include = null, int? skip = null, int? take = null)
        {
            var list = new List<Review>();
            list.Add(review);
            return Task.FromResult((IEnumerable<Review>)list);
        }
        public Task<bool> HasAnyAsync(Expression<Func<Review, bool>> filter)
        {
            return Task.FromResult(false);
        }

        public TResult QueryData<TResult>(Func<IQueryable<Review>, TResult> queryParm, Expression<Func<Review, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public Task<Review> RegisterAsync(Review model)
        {
            return Task.FromResult<Review>(model);
        }

        public Task<Review> UpdateAsync(Review model)
        {
            return Task.FromResult<Review>(model);
        }
    }
}
