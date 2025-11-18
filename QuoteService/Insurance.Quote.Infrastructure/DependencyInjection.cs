using Insurance.Quote.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Quote.Domain.Interfaces.Repositories;

namespace Insurance.Quote.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IQuoteRepository, QuoteRepository>();
        return services;
    }
}
