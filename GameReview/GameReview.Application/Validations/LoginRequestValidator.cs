using FluentValidation;
using GameReview.Application.ViewModels.Login;

namespace GameReview.Application.Validations
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(l => l.UserName)
                .Length(3, 200)
                .NotEmpty();

            RuleFor(l => l.Password)
                .Length(3, 200)
                .NotEmpty();
        }
    }
}
