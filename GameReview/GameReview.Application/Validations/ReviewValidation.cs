using FluentValidation;
using GameReview.Application.ViewModels.Review;
using GameReview.Domain.Interfaces.Repositories;

namespace GameReview.Application.Validations
{
    public class ReviewValidation : AbstractValidator<ReviewRequest>
    {
        public ReviewValidation(IUserRepository userRepository, IGameRepository gameRepository)
        {

            RuleFor(x => x.UserReview)
                .MaximumLength(255);

            RuleFor(x => x.Score)
                .NotNull().WithMessage("{PropertyName} não pode ser nula")
                .GreaterThanOrEqualTo(0)
                .LessThanOrEqualTo(10)
                .WithMessage("{PropertyName}: Deve ser entre 0 e 10");

            RuleFor(x => x.GameId)
                .NotEmpty().WithMessage("{PropertyName} não pode ser vazia")
                .NotEqual(0);

            RuleFor(x => x.GameId)
               .MustAsync((gameId, cancellationToken) => gameRepository.HasAnyAsync(x => x.Id == gameId))
               .WithMessage("{PropertyName} Id não encontrado");
        }
    }
}
