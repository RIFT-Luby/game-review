using GameReview.Application.ViewModels.Game;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace GameReview.Application.Interfaces
{
    public interface IGameService
    {
        Task<GameResquest> RegisterAsync(GameResquest gameResquest);
        Task<GameResquest> UpdateAsync(GameResquest gameResquest, int id);
        Task<GameResquest> DeleteAsync(GameResquest gameResquest);
        Task<GameResquest?> FirstAsync(Expression<Func<GameResquest, bool>> filter, 
            Func<IQueryable<GameResquest>, IIncludableQueryable<GameResquest, object>>? include = null);
        Task<IEnumerable<GameResquest>> GetDataAsync(
            Expression<Func<GameResquest, bool>>? filter = null, Func<IQueryable<GameResquest>,
            IIncludableQueryable<GameResquest, object>>? include = null,
            int? skip = null, int? take = null);
    }
}
