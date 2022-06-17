using GameReview.API.Configuration;
using GameReview.API.Filters;
using GameReview.Application.Mappers;

var builder = WebApplication.CreateBuilder(args);


builder.Services.ResolveDependencies(builder.Configuration);

builder.Services.AddAutoMapper(typeof(DomainToResponseProfile), typeof(RequestToDomainProfile));

builder.Services.AddControllers( opts =>
{
    opts.Filters.Add(new ApplicationExceptionFilter());
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
