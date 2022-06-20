using GameReview.Application.ViewModels;
using GameReview.Application.ViewModels.UserViews;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameReview.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserResponse> RegisterAsync(CreateUserRequest model);
        Task<UserResponse> UpdateAsync(UserRequest model, int id);
        Task<UserResponse> UpdatePasswordAsync(PasswordRequest model, int id);
        Task<UserResponse> RemoveAsync(int id);
        Task<UserResponse> GetByIdAsync(int id);
        Task<UserResponse> GetByIdWithReviewsAsync(int id);
        Task<IEnumerable<UserResponse>> GetAll(int? skip = null, int? take = null);
        Task<UserResponse> UploadImg(int id, IFormFile img);
        Task<UserResponse> RemoveImg(int id);
        FileStream GetImg(int id);

    }
}
 