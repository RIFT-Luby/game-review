
using FluentValidation;
using GameReview.Application.ViewModels.Game;
using GameReview.Domain.Core;
using GameReview.Domain.Models.Enumerations;

namespace GameReview.Application.Validations
{
    public class GameValidator : AbstractValidator<GameResquest>
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

            RuleFor(g => g.Score)
                .NotEmpty();

            RuleFor(g => g.Console)
                .Length(5, 100)
                .NotEmpty();

            RuleFor(g => g.GameGenderResponse)
                .Must(gender => Enumeration.GetAll<GameGender>().Any(x => x.Id == gender.Id))
                .WithMessage("Game Gender not existis");

        }
    }
}
