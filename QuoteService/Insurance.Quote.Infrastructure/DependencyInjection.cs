using Amazon;
using Amazon.Runtime;
using Amazon.SimpleNotificationService;
using Insurance.Quote.Domain.Interfaces.Ports;
using Insurance.Quote.Infrastructure.Adapters;
using Insurance.Quote.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quote.Domain.Interfaces.Repositories;

namespace Insurance.Quote.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("QuoteConnection");

        services.AddDbContext<QuoteDbContext>(options =>
            options.UseNpgsql(connectionString, b =>
                b.MigrationsAssembly(typeof(QuoteDbContext).Assembly.FullName))
            );

        services.AddSingleton<IAmazonSimpleNotificationService>(sp =>
        {
            var serviceUrl = configuration.GetSection("AWS:ServiceUrl");
            var awsCredentials = new BasicAWSCredentials("aws-credentials", "password");
            var config = new AmazonSimpleNotificationServiceConfig
            {
                ServiceURL = serviceUrl.Value,
                RegionEndpoint = RegionEndpoint.USEast1
            };

            return new AmazonSimpleNotificationServiceClient(awsCredentials, config);
        });

        services.AddScoped<IQuoteRepository, QuoteRepository>();
        services.AddScoped<IQuoteNotificationPort, QuoteSnsAdapter>();

        return services;
    }
}
