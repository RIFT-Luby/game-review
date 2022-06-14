using FluentValidation.AspNetCore;
using GameReview.Application.Interfaces;
using GameReview.Application.Services;
using GameReview.Application.Validations;

namespace GameReview.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection service)
        {
            service.AddScoped<IGameService, GameService>();

            service.AddFluentValidation(fv 
                => fv.RegisterValidatorsFromAssemblyContaining<GameValidator>());

            return service;
        }
    }
}
