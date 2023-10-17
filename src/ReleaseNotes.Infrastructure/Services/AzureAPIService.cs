using System.Text.Json;
using System.Text.Json.Serialization;
using ReleaseNotes.Infrastructure.Gateways;
using Microsoft.Extensions.Options;

namespace ReleaseNotes.Infrastructure;

public sealed class AzureAPIService : IAzureAPIService
{
    private readonly IApiGateway _apiGateway;

    public AzureAPIService(IApiGateway apiGateway, IOptions<ApiOptions> apiOptions)
    {
        _apiGateway = apiGateway;
    }

    public async Task<string> GetProjecsAsync(string organization, CancellationToken cancellationToken)
    {
        var url = $"/{organization}/_apis/projects?api-version=6.0";
        var response = await _apiGateway.GetAsync(url, cancellationToken);
        EnsureSuccessStatusCode(response);

        var content = await response.Content.ReadAsStringAsync(cancellationToken);
        var projects = JsonSerializer.Deserialize<Projects>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        return string.Join(", ", projects.Value.Select(x => x.Name));
    }

    public async Task<string> GetProjectNameAsync(string organization, string project, CancellationToken cancellationToken)
    {
        var url = $"/{organization}/{project}/_apis/projects?api-version=6.0";
        var response = await _apiGateway.GetAsync(url, cancellationToken);
        var content = await response.Content.ReadAsStringAsync(cancellationToken);
        EnsureSuccessStatusCode(response);

        var projects = JsonSerializer.Deserialize<Projects>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        return projects.Value.FirstOrDefault()?.Name;
    }

    public async Task<string> GetProjectIdAsync(string organization, string project, CancellationToken cancellationToken)
    {
        var url = $"/{organization}/{project}/_apis/projects?api-version=6.0";
        var response = await _apiGateway.GetAsync(url, cancellationToken);
        EnsureSuccessStatusCode(response);

        var content = await response.Content.ReadAsStringAsync(cancellationToken);
        var projects = JsonSerializer.Deserialize<Projects>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        return projects.Value.FirstOrDefault()?.Id;
    }

    public async Task<string> GetRepositoryIdAsync(string organization, string project, string repository, CancellationToken cancellationToken)
    {
        var url = $"/{organization}/{project}/_apis/git/repositories/{repository}?api-version=6.0";
        var response = await _apiGateway.GetAsync(url, cancellationToken);
        EnsureSuccessStatusCode(response);

        var content = await response.Content.ReadAsStringAsync(cancellationToken);
        var repositories = JsonSerializer.Deserialize<Repositories>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        return repositories.Id;
    }

    public async Task<string> GetRepositoryNameAsync(string organization, string project, string repository, CancellationToken cancellationToken)
    {
        var url = $"/{organization}/{project}/_apis/git/repositories/{repository}?api-version=6.0";
        var response = await _apiGateway.GetAsync(url, cancellationToken);
        EnsureSuccessStatusCode(response);

        var content = await response.Content.ReadAsStringAsync(cancellationToken);
        var repositories = JsonSerializer.Deserialize<Repositories>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        return repositories.Name;
    }

    public async Task<string> GetPullRequestIdAsync(string organization, string project, string repository, string pullRequest, CancellationToken cancellationToken)
    {
        var url = $"/{organization}/{project}/_apis/git/repositories/{repository}/pullRequests/{pullRequest}?api-version=6.0";
        var response = await _apiGateway.GetAsync(url, cancellationToken);
        EnsureSuccessStatusCode(response);

        var content = await response.Content.ReadAsStringAsync(cancellationToken);
        var pullRequests = JsonSerializer.Deserialize<PullRequests>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        return pullRequests.PullRequestId;
    }

    public async Task<string> GetPullRequestTitleAsync(string organization, string project, string repository, string pullRequest, CancellationToken cancellationToken)
    {
        var url = $"/{organization}/{project}/_apis/git/repositories/{repository}/pullRequests/{pullRequest}?api-version=6.0";
        var response = await _apiGateway.GetAsync(url, cancellationToken);
        EnsureSuccessStatusCode(response);

        var content = await response.Content.ReadAsStringAsync(cancellationToken);
        var pullRequests = JsonSerializer.Deserialize<PullRequests>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        return pullRequests.Title;
    }

    public async Task<string> GetPullRequestDescriptionAsync(string organization, string project, string repository, string pullRequest, CancellationToken cancellationToken)
    {
        var url = $"/{organization}/{project}/_apis/git/repositories/{repository}/pullRequests/{pullRequest}?api-version=6.0";
        var response = await _apiGateway.GetAsync(url, cancellationToken);
        EnsureSuccessStatusCode(response);

        var content = await response.Content.ReadAsStringAsync(cancellationToken);
        var pullRequests = JsonSerializer.Deserialize<PullRequests>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        return pullRequests.Description;
    }

    public async Task<string> GetWorkItemsAsync(string organization, string project, string repository, string pullRequest, CancellationToken cancellationToken)
    {
        var url = $"/{organization}/{project}/_apis/git/repositories/{repository}/pullRequests/{pullRequest}/workitems?api-version=6.0";
        var response = await _apiGateway.GetAsync(url, cancellationToken);
        EnsureSuccessStatusCode(response);

        var content = await response.Content.ReadAsStringAsync(cancellationToken);
        var workItems = JsonSerializer.Deserialize<WorkItems>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        return string.Join(", ", workItems.Value.Select(x => x.Id));
    }

    // private class WorkItemDetailsResponseConverter : JsonConverter<WorkItemDetailsResponse>
    // {
    //     public override WorkItemDetailsResponse Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    //     {
    //         var workItemDetailsResponse = new WorkItemDetailsResponse();
    //         var workItemDetails = new List<WorkItemDetails>();

    //         while (reader.Read())
    //         {
    //             if (reader.TokenType == JsonTokenType.StartObject)
    //             {
    //                 var workItemDetail = new WorkItemDetails();
    //                 while (reader.Read())
    //                 {
    //                     if (reader.TokenType == JsonTokenType.PropertyName)
    //                     {
    //                         var propertyName = reader.GetString();
    //                         reader.Read();
    //                         switch (propertyName)
    //                         {
    //                             case "id":
    //                                 workItemDetail.Id = reader.GetString();
    //                                 break;
    //                             case "fields":
    //                                 while (reader.Read())
    //                                 {
    //                                     if (reader.TokenType == JsonTokenType.PropertyName)
    //                                     {
    //                                         var fieldName = reader.GetString();
    //                                         reader.Read();
    //                                         switch (fieldName)
    //                                         {
    //                                             case "System.Title":
    //                                                 workItemDetail.Title = reader.GetString();
    //                                                 break;
    //                                             case "System.Description":
    //                                                 workItemDetail.Description = reader.GetString();
    //                                                 break;
    //                                         }
    //                                     }
    //                                     else if (reader.TokenType == JsonTokenType.EndObject)
    //                                     {
    //                                         break;
    //                                     }
    //                                 }
    //                                 break;
    //                         }
    //                     }
    //                     else if (reader.TokenType == JsonTokenType.EndObject)
    //                     {
    //                         break;
    //                     }
    //                 }
    //                 workItemDetails.Add(workItemDetail);
    //             }
    //             else if (reader.TokenType == JsonTokenType.EndArray)
    //             {
    //                 break;
    //             }
    //         }

    //         workItemDetailsResponse.Value = workItemDetails;
    //         return workItemDetailsResponse;
    //     }

    //     public override void Write(Utf8JsonWriter writer, WorkItemDetailsResponse value, JsonSerializerOptions options)
    //     {
    //         throw new NotImplementedException();
    //     }
    // }

    // private class WorkItemDetailsRequestConverter : JsonConverter<WorkItemDetailsRequest>
    // {
    //     public override WorkItemDetailsRequest Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    //     {
    //         var workItemDetailsRequest = new WorkItemDetailsRequest();
    //         var ids = new List<int>();

    //         while (reader.Read())
    //         {
    //             if (reader.TokenType == JsonTokenType.StartObject)
    //             {
    //                 while (reader.Read())
    //                 {
    //                     if (reader.TokenType == JsonTokenType.PropertyName)
    //                     {
    //                         var propertyName = reader.GetString();
    //                         reader.Read();
    //                         switch (propertyName)
    //                         {
    //                             case "id":
    //                                 ids.Add(reader.GetInt32());
    //                                 break;
    //                         }
    //                     }
    //                     else if (reader.TokenType == JsonTokenType.EndObject)
    //                     {
    //                         break;
    //                     }
    //                 }
    //             }
    //             else if (reader.TokenType == JsonTokenType.EndArray)
    //             {
    //                 break;
    //             }
    //         }

    //         workItemDetailsRequest.Ids = ids;
    //         return workItemDetailsRequest;
    //     }
    // }

}
