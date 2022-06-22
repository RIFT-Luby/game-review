using GameReview.Application.ViewModels.Login;
using System.Security.Claims;

namespace GameReview.Application.Interfaces
{
    public interface ILoginService
    {
        Task<IEnumerable<Claim>> Login(LoginRequest login);
    }
}
