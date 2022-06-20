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
    public class TempGameRepository : IGameRepository
    {
        Game game = new Game()
        {
            Id = 1,
            Name = "Game1",
            Summary = "Summary",
            Developer = "Develop",
            GameGender = null,
            GameGenderId = 1,
            Score = 5,
            Console = "PS4"

        };
        public Task DeleteAsync(Game model)
        {
            return Task.CompletedTask;
        }

        public Task<Game> FirstAsync(Expression<Func<Game, bool>> filter, Func<IQueryable<Game>, IIncludableQueryable<Game, object>> include = null)
        {
            return Task.FromResult(game);
        }

        public Task<Game> FirstAsyncAsTracking(Expression<Func<Game, bool>> filter, Func<IQueryable<Game>, IIncludableQueryable<Game, object>> include = null)
        {
            return Task.FromResult(game);
        }

        public Task<IEnumerable<Game>> GetDataAsync(Expression<Func<Game, bool>> filter = null, Func<IQueryable<Game>, IIncludableQueryable<Game, object>> include = null, int? skip = null, int? take = null)
        {
            var list = new List<Game>();
            list.Add(game);
            return Task.FromResult((IEnumerable<Game>)list);
        }

        public Task<bool> HasAnyAsync(Expression<Func<Game, bool>> filter)
        {
            return Task.FromResult(false);
        }

        public TResult QueryData<TResult>(Func<IQueryable<Game>, TResult> queryParm, Expression<Func<Game, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public Task<Game> RegisterAsync(Game model)
        {
            return Task.FromResult(game);
        }

        public Task<Game> UpdateAsync(Game model)
        {
            return Task.FromResult(game);
        }
    }
}
