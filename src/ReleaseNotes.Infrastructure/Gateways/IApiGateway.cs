namespace ReleaseNotes.Infrastructure.Gateways;

public interface IApiGateway
{
    Task<HttpResponseMessage> GetAsync(string url, CancellationToken cancellationToken);

    Task<HttpResponseMessage> PostAsync(string url, object data, CancellationToken cancellationToken);

    Task<HttpResponseMessage> PutAsync(string url, object data, CancellationToken cancellationToken);

    Task<HttpResponseMessage> DeleteAsync(string url, CancellationToken cancellationToken);

    Task<HttpResponseMessage> PatchAsync(string url, object data, CancellationToken cancellationToken);

    Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken);
}
