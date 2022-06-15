using GameReview.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameReview.Domain.Models.Enumerations
{
    public class UserRole: Enumeration
    {
        public static UserRole Admin = new UserRole(1, nameof(Admin));
        public static UserRole Commom = new UserRole(2, nameof(Commom));

        public UserRole() { }
        private UserRole(int id, string name) : base(id, name) { }

    }
}
