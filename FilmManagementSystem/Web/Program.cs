using ApplicationCore.Interfaces;
using ApplicationCore.Mappers;
using ApplicationCore.Services;
using FluentValidation;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Web.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

//middlewares
builder.Services.AddScoped<GlobalExceptionHandler>();
//repositories
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
//services
builder.Services.AddScoped<IFilmService, FilmService>();
//Mappers
builder.Services.AddMapster();
MapsterConfig.FilmMappings();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddValidatorsFromAssembly(Assembly.Load("ApplicationCore"));

//cors
builder.Services.AddCors(options =>
{
    var corsPolicySection = builder.Configuration.GetSection("CorsPolicy:AllowReactApp");
    var allowedOrigins = corsPolicySection.GetSection("AllowedOrigins").Get<string[]>() ?? Array.Empty<string>();
    options.AddPolicy("AllowReactApp", policyBuilder =>
    {
        policyBuilder.WithOrigins(allowedOrigins)
                      .AllowAnyMethod()
                      .AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseCors("AllowReactApp");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseMiddleware<GlobalExceptionHandler>();
app.MapControllers();

app.Run();
