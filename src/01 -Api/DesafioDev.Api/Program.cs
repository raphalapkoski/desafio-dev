using DesafioDev.Application;
using DesafioDev.Infra.Persistence;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddApplication().AddRepositories(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddHealthChecks();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "DesafioDev",
        Version = "v1",
        Description = "DevChallenge: to integrate your system with our api, follow the end-points guide below, which contains the type, request body, return types and type of data expected in the operation."
    });
    c.EnableAnnotations();
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

if (app.Environment.IsProduction())
{
    app.UseHealthChecks("/health");
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.RunMigration();

app.Run();
