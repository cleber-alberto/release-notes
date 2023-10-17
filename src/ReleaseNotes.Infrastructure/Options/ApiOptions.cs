namespace ReleaseNotes.Infrastructure;

public class ApiOptions
{
    public const string SectionName = "Api";

    public string BaseAddress { get; set; } = string.Empty;
    public string Authorization { get; set; } = string.Empty;
}
