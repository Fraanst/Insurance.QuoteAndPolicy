using Insurance.Policy.Domain.Interfaces.Repositories;
using Insurance.Policy.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Insurance.Policy.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IPolicyRepository, PolicyRepository>();
        return services;
    }
}
