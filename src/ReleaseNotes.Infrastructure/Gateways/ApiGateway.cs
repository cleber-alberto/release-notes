
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;

namespace ReleaseNotes.Infrastructure.Gateways;

public class ApiGateway : IApiGateway
{
    private readonly IHttpClientFactory _httpClientFactory = null!;
    private readonly HttpClient _httpClient;

    public ApiGateway(IHttpClientFactory httpClientFactory, IOptions<ApiOptions> apiOptions)
    {
        _httpClientFactory = httpClientFactory;
        _httpClient = _httpClientFactory.CreateClient();
        _httpClient.BaseAddress = new Uri(apiOptions.Value.BaseAddress);
        _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        _httpClient.DefaultRequestHeaders.Add("User-Agent", "ReleaseNotes");
        _httpClient.DefaultRequestHeaders.Add("X-Api-Version", "6.0");
        _httpClient.DefaultRequestHeaders.Add("X-Api-Preview", "true");
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Basic {apiOptions.Value.Authorization}");
    }

    public Task<HttpResponseMessage> DeleteAsync(string url, CancellationToken cancellationToken)
        => _httpClient.DeleteAsync(url, cancellationToken);

    public Task<HttpResponseMessage> GetAsync(string url, CancellationToken cancellationToken)
        => _httpClient.GetAsync(url, cancellationToken);

    public Task<HttpResponseMessage> PatchAsync(string url, object data, CancellationToken cancellationToken)
        => _httpClient.PatchAsync(url, new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json"), cancellationToken);

    public Task<HttpResponseMessage> PostAsync(string url, object data, CancellationToken cancellationToken)
        => _httpClient.PostAsync(url, new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json"), cancellationToken);

    public Task<HttpResponseMessage> PutAsync(string url, object data, CancellationToken cancellationToken)
        => _httpClient.PutAsync(url, new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json"), cancellationToken);

    public Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        => _httpClient.SendAsync(request, cancellationToken);
}
