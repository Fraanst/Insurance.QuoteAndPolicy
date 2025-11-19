using Amazon;
using Amazon.Runtime;
using Amazon.SimpleNotificationService;
using Insurance.Quote.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Quote.Domain.Interfaces.Repositories;


namespace Insurance.Quote.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IQuoteRepository, QuoteRepository>();
        services.AddSingleton<IAmazonSimpleNotificationService>(sp =>
        {
            var awsCredentials = new BasicAWSCredentials("quote-admin", "quote-password");
            var config = new AmazonSimpleNotificationServiceConfig
            {
                ServiceURL = "",
                RegionEndpoint = RegionEndpoint.USEast1
            };

            return new AmazonSimpleNotificationServiceClient(awsCredentials, config);
        });
        
        return services;
    }
}
