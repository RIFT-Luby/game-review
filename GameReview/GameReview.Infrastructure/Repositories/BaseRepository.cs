﻿using GameReview.Domain.Core;
using GameReview.Domain.Interfaces;
using GameReview.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace GameReview.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : Register, new()
    {
        protected readonly ApplicationContext _context;
        private readonly DbSet<T> _dbSet;
        public BaseRepository(ApplicationContext context)
        {
            _context = context;
        }
        public virtual async Task<T> RegisterAsync(T model)
        {
            model.CreatedAt = DateTime.Now;
            model.UpdatedAt = DateTime.Now;
            var result = _context.Set<T>().Add(model).Entity;
            return await Task.FromResult(result);
        }

        public virtual async Task<T> UpdateAsync(T model)
        {
            model.UpdatedAt = DateTime.Now;
            var result = _context.Set<T>().Update(model).Entity;
            return await Task.FromResult(result);
        }

        public virtual Task DeleteAsync(T model)
        {
            _context.Set<T>().Remove(model);
            return Task.CompletedTask;
        }

        public async Task<T?> FirstAsync(Expression<Func<T, bool>> filter, Func<IQueryable<T>, 
            IIncludableQueryable<T, object>>? include = null)
        {
            var query = _context.Set<T>().AsQueryable();
            if (include != null)
                query = include(query);

            return await query.FirstOrDefaultAsync(filter);
        }

        public async Task<T?> FirstAsyncAsTracking(Expression<Func<T, bool>> filter, Func<IQueryable<T>, 
            IIncludableQueryable<T, object>>? include = null)
        {
            var query = _context.Set<T>().AsQueryable();
            if (include != null)
                query = include(query);

            return await query.AsTracking().FirstOrDefaultAsync(filter);
        }

        public async Task<IEnumerable<T>> GetDataAsync(Expression<Func<T, bool>>? filter = null, 
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, int? skip = null, int? take = null)
        {
            var query = _context.Set<T>().AsQueryable();
            if (filter != null)
                query = query.Where(filter);

            if (include != null)
                query = include(query);

            if (skip != null && skip.HasValue)
                query = query.Skip(skip.Value);

            if (take != null && take.HasValue)
                query = query.Take(take.Value);

            return await query.ToListAsync();
        }

        public TResult QueryData<TResult>(Func<IQueryable<T>, TResult> queryParm, Expression<Func<T, bool>>? filter = null)
        {
            var query = _context.Set<T>().AsQueryable<T>();
            if (filter != null)
                query = query.Where(filter);

            var result = queryParm(query);
            return result;
        }

        public async Task<bool> HasAnyAsync(Expression<Func<T, bool>> filter)
        {
            var result = await _context.Set<T>().AnyAsync(filter);
            return result;
        }

        public async Task<int> CountAll(Expression<Func<T, bool>>? filter = null)
        {
            var query = _context.Set<T>().AsQueryable();
            if (filter != null)
                query = query.Where(filter);

            return await query.CountAsync();
        }
    }
}
