
using GameReview.Application.Interfaces;
using GameReview.Application.ViewModels.Game;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace GameReview.Application.Services
{
    public class GameService : IGameService
    {
        public Task<GameResquest> RegisterAsync(GameResquest gameResquest)
        {
            throw new NotImplementedException();
        }

        public Task<GameResquest> UpdateAsync(GameResquest gameResquest, int id)
        {
            throw new NotImplementedException();
        }

        public Task<GameResquest> DeleteAsync(GameResquest gameResquest)
        {
            throw new NotImplementedException();
        }

        public Task<GameResquest?> FirstAsync(Expression<Func<GameResquest, bool>> filter, Func<IQueryable<GameResquest>, IIncludableQueryable<GameResquest, object>>? include = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<GameResquest>> GetDataAsync(Expression<Func<GameResquest, bool>>? filter = null, Func<IQueryable<GameResquest>, IIncludableQueryable<GameResquest, object>>? include = null, int? skip = null, int? take = null)
        {
            throw new NotImplementedException();
        }
       
    }
}
