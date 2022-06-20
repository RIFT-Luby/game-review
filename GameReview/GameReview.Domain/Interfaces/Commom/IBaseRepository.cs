using GameReview.Domain.Core;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace GameReview.Domain.Interfaces
{
    public interface IBaseRepository<T> where T : Register
    {
        Task<T> RegisterAsync(T model);
        Task<T> UpdateAsync(T model);
        Task DeleteAsync(T model);
        Task<T?> FirstAsync(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);
        Task<T?> FirstAsyncAsTracking(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);
        Task<IEnumerable<T>> GetDataAsync(
            Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>,
            IIncludableQueryable<T, object>>? include = null,
            int? skip = null, int? take = null);
        TResult QueryData<TResult>(Func<IQueryable<T>, TResult> queryParm, Expression<Func<T, bool>>? filter = null);
        Task<bool> HasAnyAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken);
    }
}
