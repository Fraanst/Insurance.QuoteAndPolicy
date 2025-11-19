using Insurance.Policy.Application.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace Insurance.Policy.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ContractQuoteHandler>();
        services.AddScoped<GetPolicyByIdHandler>();

        return services;
    }
}
