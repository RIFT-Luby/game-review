using GameReview.Application.Interfaces;
using GameReview.Application.Services;

namespace GameReview.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection service)
        {
            service.AddScoped<IReviewService, ReviewService>();
            return service;
        }
    }
}
