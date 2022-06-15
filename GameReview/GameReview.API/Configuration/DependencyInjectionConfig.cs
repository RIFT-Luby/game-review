using FluentValidation.AspNetCore;
using GameReview.Application.Interfaces;
using GameReview.Application.Services;
using GameReview.Application.Validations;
using GameReview.Domain.Interfaces.Repositories;
using GameReview.Infrastructure.Repositories;
using GameReview.Domain.Interfaces.Commom;
using GameReview.Infrastructure.UnitOfWork;

namespace GameReview.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection service)
        {
            //repositories
            service.AddScoped<IUserRepository, TempUserRepository>();
            service.AddScoped<IGameRepository, TempGameRepository>();
            service.AddScoped<IReviewRepository, TempReviewRepository>();

            //services
            service.AddScoped<IUserService, UserService>();
            service.AddScoped<IGameService, GameService>();
            service.AddScoped<IReviewService, ReviewService>();

            //uow
            service.AddScoped<IUnitOfWork, UnitOfWork>();

            //validators
            service.AddFluentValidation(fv =>
            {
                fv.AutomaticValidationEnabled = false;
                fv.RegisterValidatorsFromAssemblyContaining<UserRequestValidator>();
            });

            

            return service;
        }
    }
}
