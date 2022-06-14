using GameReview.Application.Interfaces;
using GameReview.Application.Services;
using FluentValidation.AspNetCore;
using GameReview.Application.Validations;

namespace GameReview.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection service)
        {
            //repositories

            //services
            service.AddScoped<IUserService, UserService>();

            //uow

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
