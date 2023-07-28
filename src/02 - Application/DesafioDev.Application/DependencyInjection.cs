using DesafioDev.Application.Interfaces;
using DesafioDev.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DesafioDev.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;

        services.AddAutoMapper(assembly);

        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(assembly);
        });

        services.AddScoped<IFileServices, FileServices>();

        return services;
    }
}
