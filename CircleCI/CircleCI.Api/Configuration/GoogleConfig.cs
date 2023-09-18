namespace CircleCI.Api.Configuration;

public class GoogleConfig
{
    public const string SectionName = "GoogleBucket";
    public string GoogleCredentialFile { get; set; } = string.Empty;
    public string GoogleCloudStorageBucket { get; set; } = string.Empty;
}