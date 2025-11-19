using Insurance.Policy.Application.Handlers;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Insurance.Policy.Application;

[ExcludeFromCodeCoverage]
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ContractQuoteHandler>();
        services.AddScoped<GetPolicyByIdHandler>();

        return services;
    }
}
