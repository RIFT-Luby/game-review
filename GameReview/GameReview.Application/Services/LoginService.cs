using FluentValidation;
using GameReview.Application.Exceptions;
using GameReview.Application.Interfaces;
using GameReview.Application.ViewModels.Login;
using GameReview.Domain.Core;
using GameReview.Domain.Interfaces.Repositories;
using GameReview.Domain.Models.Enumerations;
using GameReview.Infrastructure.Utils;
using System.Security.Claims;

namespace GameReview.Application.Services
{
    public class LoginService : ILoginService
    {
        private IUserRepository _userRepository;

        public LoginService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<Claim>> Login(LoginRequest login)
        {
            var result = await _userRepository.FirstAsync(filter: x => x.UserName == login.UserName) ?? throw new NotFoundRequestException($"Usuário {login.UserName} não encontrado");

            if (!PasswordHasher.Verify(login.Password, result.Password))
                throw new BadRequestException(nameof(login.Password), "Senha invalida");

            return new List<Claim>
            {
                new Claim(ClaimTypes.Sid, result.Id.ToString()),
                new Claim(ClaimTypes.Email, result.Email),
                new Claim(ClaimTypes.Name, result.Name),
                new Claim(ClaimTypes.Role, Enumeration.FromId<UserRole>(result.UserRoleId).Name)
            };
        } 
    }
}
