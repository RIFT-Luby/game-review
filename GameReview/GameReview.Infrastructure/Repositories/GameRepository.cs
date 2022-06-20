using GameReview.Domain.Interfaces.Repositories;
using GameReview.Domain.Models;
using GameReview.Infrastructure.Context;

namespace GameReview.Infrastructure.Repositories
{
    public class GameRepository : BaseRepository<Game>, IGameRepository
    {
        public GameRepository(ApplicationContext context) : base(context)
        {
            
        }
    }
}
