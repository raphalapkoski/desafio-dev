using DesafioDev.Domain.Repositories;
using DesafioDev.Infra.Persistence.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DesafioDev.Infra.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
        var dbName = Environment.GetEnvironmentVariable("DB_NAME");
        var dbSaPassword = Environment.GetEnvironmentVariable("DB_SA_PASSWORD");
        var connectionString = configuration.GetSection("Environment")?.Value != "Development" 
            ? $"Data Source={dbHost};Initial Catalog={dbName}; User ID=sa;Password={dbSaPassword};TrustServerCertificate=True"
            : configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        services.AddTransient<IEstablishmentRepository, EstablishmentRepository>();
        return services;
    }

    public static void RunMigration(this IApplicationBuilder app)
    {
        using var provider = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
        using var context = provider.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        //context.Database.Migrate();
    }
}
