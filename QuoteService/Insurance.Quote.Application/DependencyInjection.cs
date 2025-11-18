using Insurance.Quote.Application.Handlers;
using Microsoft.Extensions.DependencyInjection;
using Quote.Application.Handlers;

namespace Insurance.Quote.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<CreateQuoteHandler>();
        services.AddScoped<GetQuoteByIdHandler>();
        services.AddScoped<ChangeQuoteStatusHandler>();
        services.AddScoped<ListQuotesHandler>();

        return services;
    }
}
