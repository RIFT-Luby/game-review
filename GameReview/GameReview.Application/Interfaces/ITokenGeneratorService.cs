
using System.Security.Claims;

namespace GameReview.Application.Interfaces
{
    public interface ITokenGeneratorService
    {
        string GenerateToken(IEnumerable<Claim> claims);
    }
}
