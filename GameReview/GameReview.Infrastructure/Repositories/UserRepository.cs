using GameReview.Domain.Interfaces.Repositories;
using GameReview.Domain.Models;
using GameReview.Infrastructure.Context;

namespace GameReview.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
