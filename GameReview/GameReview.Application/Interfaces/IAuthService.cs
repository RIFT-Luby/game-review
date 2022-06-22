using System.Security.Claims;

namespace GameReview.Application.Interfaces
{
    public interface IAuthService
    {
        int Id { get; }
        string Name { get; }
        string Email { get; }
        IEnumerable<Claim> GetClaims();
    }
}
