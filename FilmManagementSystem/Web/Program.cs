using ApplicationCore.Interfaces;
using ApplicationCore.Mappers;
using ApplicationCore.Services;
using FluentValidation;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Infrastructure.Repositories;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
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
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "FilmManagementSystem API",
        Version = "v1",
        Description = "API for managing films"
    });
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

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
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Film API v1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseMiddleware<GlobalExceptionHandler>();
app.MapControllers();

app.Run();
