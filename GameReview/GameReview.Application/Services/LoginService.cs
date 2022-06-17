using FluentValidation;
using GameReview.Application.Exceptions;
using GameReview.Application.Interfaces;
using GameReview.Application.ViewModels.Login;
using GameReview.Domain.Interfaces.Repositories;
using System.Security.Claims;

namespace GameReview.Application.Services
{
    public class LoginService : ILoginService
    {
        private IUserRepository _userRepository;
        private IValidator<LoginRequest> _validator;

        public LoginService(IUserRepository userRepository,
                            IValidator<LoginRequest> validator)
        {
            _userRepository = userRepository;
            _validator = validator;
        }

        public async Task<IEnumerable<Claim>> Login(LoginRequest login)
        {

            var validationResult = await _validator.ValidateAsync(login);

            if (!validationResult.IsValid)
                throw new BadRequestException(validationResult);

            var result = await _userRepository.FirstAsync(filter: x => x.UserName == login.UserName)
                ?? throw new NotFoundRequestException($"Usuário {login.UserName} não encontrado");

            if (result.UserName != login.UserName || login.Password != result.Password)
                throw new BadRequestException("Nome de usuário ou senha estão incorretos. Por favor. Tente novamente");

            return new List<Claim>
            {
                new Claim(ClaimTypes.Sid, result.Id.ToString()),
                new Claim(ClaimTypes.Email, result.Email),
                new Claim(ClaimTypes.Name, result.Name),
                new Claim(ClaimTypes.Role, result.UserRole.Name)
            };
        }
    }
}
