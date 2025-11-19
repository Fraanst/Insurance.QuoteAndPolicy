using Insurance.Policy.Domain.Interfaces.Ports;
using Insurance.Policy.Domain.Interfaces.Repositories;
using Insurance.Policy.Infrastructure.Adapters;
using Insurance.Policy.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Insurance.Policy.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("PolicyConnection");
        services.AddDbContext<PolicyDbContext>(options =>
        options.UseNpgsql(connectionString, b =>
            b.MigrationsAssembly(typeof(PolicyDbContext).Assembly.FullName))
        );

        services.AddHttpClient<IQuoteServicePort, QuoteServiceHttpAdapter>(client =>
        {
            client.BaseAddress = new Uri(configuration.GetSection("QuoteService:BaseUrl").Value);
        }) ;

        services.AddScoped<IPolicyRepository, PolicyRepository>();
        return services;
    }
}
