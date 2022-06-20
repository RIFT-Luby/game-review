using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameReview.Application.ViewModels.UserViews
{
    public class UserRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public int UserRoleId { get; set; }
    }
}
