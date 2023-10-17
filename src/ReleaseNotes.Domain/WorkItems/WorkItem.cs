namespace ReleaseNotes.Domain.WorkItems;

public sealed class WorkItem
{
    public string Id { get; set; } = string.Empty;
    WorkItemDetails Details { get; set; } = new WorkItemDetails();
}
