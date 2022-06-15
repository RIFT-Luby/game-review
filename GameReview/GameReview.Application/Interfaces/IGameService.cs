using GameReview.Application.ViewModels.Game;
using GameReview.Application.ViewModels.GameGender;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace GameReview.Application.Interfaces
{
    public interface IGameService
    {
        Task<GameResponse> RegisterAsync(GameResquest gameResquest);
        Task<GameResponse> UpdateAsync(GameResquest gameResquest, int id);
        Task<GameResponse> DeleteAsync(int id);
        Task<GameResponse> GetById(int id);
        Task<IEnumerable<GameResponse>> GetAll();
        Task<IEnumerable<GameGenderResponse>> GetAllGender();
    }
}
