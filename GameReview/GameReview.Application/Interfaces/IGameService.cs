using GameReview.Application.Params;
using GameReview.Application.ViewModels.Game;
using GameReview.Application.ViewModels.GameGender;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace GameReview.Application.Interfaces
{
    public interface IGameService
    {
        Task<GameResponse> RegisterAsync(GameRequest gameResquest);
        Task<GameResponse> UpdateAsync(GameRequest gameResquest, int id);
        Task<GameResponse> DeleteAsync(int id);
        Task<GameResponse> GetById(int id);
        Task<IEnumerable<GameResponse>> GetAll(GameParams query);
        Task<IEnumerable<GameGenderResponse>> GetAllGender();
        Task<GameResponse> UploadImg(int id, IFormFile img);
        Task<GameResponse> RemoveImg(int id);
        FileStream GetImg(int id);
    }
}
