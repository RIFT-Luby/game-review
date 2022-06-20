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


        User adminUser = new User()
        {
            Id = 1,
            Name = "FirstTeste",
            UserName = "Admin",
            Email = "igor-coura@hotmail.com",
            Password = "AQAAAAEAAAPoAAAAEMxHFcvNGDwbhbop+wAZap5Mrxm2hQ1mdus5TVIKYCM2/IUl9YkqNHqZxp2r7/oQFg==",
            UserRoleId = 1,
        };

        User comumUser = new User()
        {
            Id = 2,
            Name = "FirstTeste",
            UserName = "Comum",
            Email = "igor-coura@hotmail.com",
            Password = "AQAAAAEAAAPoAAAAEFoaMdknLAdDiqXj9z2ruLhGv/unSxW7Ys6H8wmb1g054oMbg6+5AHee7EUTimtF9g==",
            UserRoleId = 2,
        };


        public Task DeleteAsync(User model)
        {
            return Task.CompletedTask;
        }

        public Task<User?> FirstAsync(Expression<Func<User, bool>> filter, Func<IQueryable<User>, IIncludableQueryable<User, object>>? include = null)
        {
            var func = filter.Compile();
            if (func(adminUser))
            {
                return Task.FromResult(
                    adminUser
                    );
            }
            if (func(comumUser))
            {
                return Task.FromResult(
                    comumUser
                    );
            }
            return Task.FromResult<User?>(null);
        }

        public Task<User?> FirstAsyncAsTracking(Expression<Func<User, bool>> filter, Func<IQueryable<User>, IIncludableQueryable<User, object>>? include = null)
        {
            var func = filter.Compile();
            if (func(adminUser))
            {
                return Task.FromResult(
                    adminUser
                    );
            }
            if (func(comumUser))
            {
                return Task.FromResult(
                    comumUser
                    );
            }
            return Task.FromResult<User?>(null);
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
