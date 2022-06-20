using GameReview.Domain.Interfaces;
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
    public class TempUserRepository : IUserRepository
    {
        User user = new User()
        {
            Id = 22,
            Name = "FirstTeste",
            Email = "FirstTeste",
            Password = "FirstTeste",
            UserRoleId = 1,
        };
        public Task DeleteAsync(User model)
        {
            return Task.CompletedTask;
        }

        public Task<User?> FirstAsync(Expression<Func<User, bool>> filter, Func<IQueryable<User>, IIncludableQueryable<User, object>>? include = null)
        {
            return Task.FromResult(user)!;
        }

        public Task<User?> FirstAsyncAsTracking(Expression<Func<User, bool>> filter, Func<IQueryable<User>, IIncludableQueryable<User, object>>? include = null)
        {
            return Task.FromResult(user)!;
        }

        public Task<IEnumerable<User>> GetDataAsync(Expression<Func<User, bool>>? filter = null, Func<IQueryable<User>, IIncludableQueryable<User, object>>? include = null, int? skip = null, int? take = null)
        {
            throw new NotImplementedException();
        }

        public Task<bool> HasAnyAsync(Expression<Func<User, bool>> filter)
        {
            return Task.FromResult(false);
        }

        public TResult QueryData<TResult>(Func<IQueryable<User>, TResult> queryParm, Expression<Func<User, bool>>? filter = null)
        {
            throw new NotImplementedException();
        }

        public Task<User> RegisterAsync(User model)
        {
            return Task.FromResult(model);
        }

        public Task<User> UpdateAsync(User model)
        {
            return Task.FromResult(model);
        }
    }
}
