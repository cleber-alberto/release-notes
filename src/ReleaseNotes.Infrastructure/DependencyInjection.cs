using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReleaseNotes.Infrastructure.Gateways;

namespace ReleaseNotes.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var apiOptions = new ApiOptions();
        configuration.GetSection(ApiOptions.SectionName).Bind(apiOptions);

        services.AddHttpClient<IApiGateway, ApiGateway>(client =>
        {
            client.BaseAddress = new Uri(apiOptions.BaseAddress);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("User-Agent", "ReleaseNotes");
            client.DefaultRequestHeaders.Add("X-Api-Version", "6.0");
            client.DefaultRequestHeaders.Add("X-Api-Preview", "true");
            client.DefaultRequestHeaders.Add("Authorization", $"Basic {apiOptions.Authorization}");
        });

        return services;
    }
}
