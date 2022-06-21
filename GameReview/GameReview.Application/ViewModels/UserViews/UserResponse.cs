using GameReview.Application.ViewModels.Review;
using GameReview.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameReview.Application.ViewModels.UserViews
{
    public class UserResponse: Register
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public int UserRoleId { get; set; }
        public UserRoleResponse UserRole { get; set; }
        public IEnumerable<ReviewResponse> Reviews { get; set; }    
    }
}
