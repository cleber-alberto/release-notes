namespace ReleaseNotes.Infrastructure;

public interface IAzureAPIService
{
    Task<string> GetProjecsAsync(string organization, CancellationToken cancellationToken);

    Task<string> GetProjectNameAsync(string organization, string project, CancellationToken cancellationToken);

    Task<string> GetProjectIdAsync(string organization, string project, CancellationToken cancellationToken);

    Task<string> GetRepositoryIdAsync(string organization, string project, string repository, CancellationToken cancellationToken);

    Task<string> GetRepositoryNameAsync(string organization, string project, string repository, CancellationToken cancellationToken);

    Task<string> GetPullRequestIdAsync(string organization, string project, string repository, string pullRequest, CancellationToken cancellationToken);

    Task<string> GetPullRequestTitleAsync(string organization, string project, string repository, string pullRequest, CancellationToken cancellationToken);

    Task<string> GetPullRequestDescriptionAsync(string organization, string project, string repository, string pullRequest, CancellationToken cancellationToken);

    Task<string> GetWorkItemsAsync(string organization, string project, string repository, string pullRequest, CancellationToken cancellationToken);
}
