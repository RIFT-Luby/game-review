<<<<<<< HEAD
﻿using FluentValidation.AspNetCore;
using GameReview.Application.Interfaces;
using GameReview.Application.Services;
using GameReview.Application.Validations;
=======
﻿using GameReview.Application.Interfaces;
using GameReview.Application.Services;
<<<<<<< HEAD
using FluentValidation.AspNetCore;
using GameReview.Application.Validations;
using GameReview.Domain.Interfaces.Repositories;
using GameReview.Infrastructure.Repositories;
using GameReview.Domain.Interfaces.Commom;
using GameReview.Infrastructure.UnitOfWork;
=======
>>>>>>> 955813aa2fa87a6871e009e01f0f19a8fabac86c
>>>>>>> 67a53f209d7772218ede0dc0eeec517d6d5c1d44

namespace GameReview.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection service)
        {
<<<<<<< HEAD
            service.AddScoped<IGameService, GameService>();

            service.AddFluentValidation(fv 
                => fv.RegisterValidatorsFromAssemblyContaining<GameValidator>());

=======
<<<<<<< HEAD
            //repositories
            service.AddScoped<IUserRepository, TempUserRepository>();

            //services
            service.AddScoped<IUserService, UserService>();

            //uow
            service.AddScoped<IUnitOfWork, UnitOfWork>();

            //validators
            service.AddFluentValidation(fv =>
            {
                fv.AutomaticValidationEnabled = false;
                fv.RegisterValidatorsFromAssemblyContaining<UserRequestValidator>();
            });

=======
            service.AddScoped<IReviewService, ReviewService>();
>>>>>>> 955813aa2fa87a6871e009e01f0f19a8fabac86c
>>>>>>> 67a53f209d7772218ede0dc0eeec517d6d5c1d44
            return service;
        }
    }
}
