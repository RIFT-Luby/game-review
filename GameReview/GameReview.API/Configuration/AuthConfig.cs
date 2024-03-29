﻿using GameReview.Application.Constants;
using GameReview.Application.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace GameReview.API.Configuration
{
    public static class AuthConfig
    {
        public static IServiceCollection AddAuthConfig(this IServiceCollection services,
            IConfiguration configuration)
        {

            //JWT
            var appsSettingsSection = configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appsSettingsSection);

            var appsSettings = appsSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appsSettings.Secret);

            services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(auth =>
            {
                auth.RequireHttpsMetadata = false;
                auth.SaveToken = true;
                auth.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = appsSettings.ValidOn,
                    ValidIssuer = appsSettings.Emissary
                };
            });

            services.AddAuthorization(auth =>
            {
                auth.AddPolicy(Roles.Admin, p => p.RequireRole(Roles.Admin));
                auth.AddPolicy(Roles.Common, p => p.RequireRole(Roles.Common));
            });

            return services;
        }
    }
}
