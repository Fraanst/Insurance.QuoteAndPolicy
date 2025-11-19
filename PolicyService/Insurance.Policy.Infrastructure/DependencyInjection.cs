using Insurance.Policy.Domain.Interfaces.Ports;
using Insurance.Policy.Domain.Interfaces.Repositories;
using Insurance.Policy.Infrastructure.Adapters;
using Insurance.Policy.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Insurance.Policy.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddHttpClient<IQuoteServicePort, QuoteServiceHttpAdapter>(client =>
        {
            // O BaseAddress será preenchido pela variável de ambiente do Docker Compose!
            // No docker-compose, você configurou: QuoteService__BaseUrl: http://quote-api:8080
            // O .NET injeta essa configuração automaticamente.
            client.BaseAddress = new Uri("http://quote-api:8080/");
        }) ;

        services.AddScoped<IPolicyRepository, PolicyRepository>();
        return services;
    }
}
