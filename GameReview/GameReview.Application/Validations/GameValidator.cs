
using FluentValidation;
using GameReview.Application.ViewModels.Game;
using GameReview.Domain.Core;
using GameReview.Domain.Models.Enumerations;

namespace GameReview.Application.Validations
{
    public class GameValidator : AbstractValidator<GameRequest>
    {
        public GameValidator()
        {
            RuleFor(g => g.Name)
                .Length(5, 100)
                .NotEmpty();

            RuleFor(g => g.Summary)
                .Length(10, 200)
                .NotEmpty();

            RuleFor(g => g.Developer)
                .Length(5, 100)
                .NotEmpty();

            RuleFor(g => g.Console)
                .Length(5, 100)
                .NotEmpty();

            RuleFor(g => g.GameGenderId)
                .Must(id => Enumeration.GetAll<GameGender>().Any(x => x.Id == id))
                .WithMessage("O genêro do jogo não existe");

        }
    }
}
