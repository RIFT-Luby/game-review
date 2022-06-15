using FluentValidation;
using GameReview.Application.ViewModels.Review;

namespace GameReview.Application.Validations
{
    public class ReviewValidation : AbstractValidator<ReviewRequest>
    {
        public ReviewValidation()
        {
            RuleFor(x => x.ReviewUser)
                .NotEmpty().WithMessage("{PropertyName} não pode ser vazia")
                .MaximumLength(255);

            RuleFor(x => x.Score)
                .NotEmpty().WithMessage("{PropertyName} não pode ser vazia")
                .GreaterThanOrEqualTo(0)
                .LessThanOrEqualTo(10)
                .WithMessage("{PropertyName}: Deve ser entre 0 e 10");

            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("{PropertyName} não pode ser vazia")
                .NotEqual(0);

            RuleFor(x => x.GameId)
                .NotEmpty().WithMessage("{PropertyName} não pode ser vazia")
                .NotEqual(0);
        }
    }
}
