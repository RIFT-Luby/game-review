using GameReview.Domain.Core;
using GameReview.Domain.Models.Enumerations;

namespace GameReview.Domain.Models
{
    public class User: Register
    {
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int UserRoleId { get; set; }
        public UserRole UserRole { get; set; }

    }
}
