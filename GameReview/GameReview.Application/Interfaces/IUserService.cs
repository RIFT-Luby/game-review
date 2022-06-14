using GameReview.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameReview.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserResponse> RegisterAsync(UserRequest model);
        Task<UserResponse> UpdateAsync(UserRequest model, int id);
        Task<UserResponse> RemoveAsync(int id);
        Task<UserResponse> GetByIdAsync(int id);
    }
}
