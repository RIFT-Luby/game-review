﻿using FluentValidation.AspNetCore;
using GameReview.Application.Interfaces;
using GameReview.Application.Services;
using GameReview.Application.Validations;
using GameReview.Domain.Interfaces.Repositories;
using GameReview.Infrastructure.Repositories;
using GameReview.Domain.Interfaces.Commom;
using GameReview.Infrastructure.UnitOfWork;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.Extensions.Options;
using GameReview.Domain.Interfaces.Storage;
using GameReview.Infrastructure.Storage;
using GameReview.Application.Options;

namespace GameReview.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection service, ConfigurationManager configuration)
        {
            //storage
            service.AddSingleton<IFileStorage, FileStorage>();

            //repositories
            service.AddScoped<IUserRepository, TempUserRepository>();
            service.AddScoped<IGameRepository, TempGameRepository>();
            service.AddScoped<IReviewRepository, TempReviewRepository>();

            //options
            service.Configure<Application.Options.FileApiOptions>(configuration.GetSection("FileSettings"));

            //services
            service.AddScoped<IUserService, UserService>();
            service.AddScoped<IGameService, GameService>();
            service.AddScoped<IReviewService, ReviewService>();
            service.AddScoped<ILoginService, LoginService>();
            service.AddScoped<ITokenGeneratorService, TokenGeneratorService>();

            //uow
            service.AddScoped<IUnitOfWork, UnitOfWork>();

            //validators
            service.AddFluentValidation(fv =>
            {
                fv.AutomaticValidationEnabled = false;
                fv.RegisterValidatorsFromAssemblyContaining<UserRequestValidator>();
            });

            service.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            return service;
        }
    }
}
