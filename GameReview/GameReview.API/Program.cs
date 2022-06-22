using GameReview.API.Configuration;
using GameReview.API.Filters;
using GameReview.Application.Mappers;
using GameReview.Infrastructure.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;
});

builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.AddDbContext<ApplicationContext>(options =>
{
    options
        .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
        .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTrackingWithIdentityResolution)
        .EnableDetailedErrors()
        .EnableSensitiveDataLogging();

});

builder.Services.AddHttpContextAccessor();

builder.Services.ResolveDependencies(builder.Configuration);

builder.Services.AddAuthConfig(builder.Configuration);

builder.Services.AddSwaggerConfig();

builder.Services.AddAutoMapper(typeof(DomainToResponseProfile), typeof(RequestToDomainProfile));

builder.Services.AddControllers(opts =>
{
    opts.Filters.Add(new ApplicationExceptionFilter());
});


builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();
var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwaggerConfig(apiVersionDescriptionProvider);

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
