using Newtonsoft.Json;

namespace CircleCI.Api.Configuration;

public class GoogleConfig
{
    public const string SectionName = "GoogleBucket";
    public Credentials GoogleCredential { get; set; } = new();
    public string GoogleCloudStorageBucket { get; set; } = string.Empty;
}

public class Credentials
{
    [ConfigurationKeyName("type")]
    [JsonProperty("type")]
    public string Type { get; set; } = string.Empty;
    [ConfigurationKeyName("project_id")]
    [JsonProperty("project_id")]
    public string ProjectId { get; set; } = string.Empty;
    [ConfigurationKeyName("private_key_id")]
    [JsonProperty("private_key_id")]
    public string PrivateKeyId { get; set; } = string.Empty;
    [ConfigurationKeyName("private_key")]
    [JsonProperty("private_key")]
    public string PrivateKey { get; set; } = string.Empty;
    [ConfigurationKeyName("client_email")]
    [JsonProperty("client_email")]
    public string ClientEmail { get; set; } = string.Empty;
    [ConfigurationKeyName("client_id")]
    [JsonProperty("client_id")]
    public string ClientId { get; set; } = string.Empty;
    [ConfigurationKeyName("auth_uri")]
    [JsonProperty("auth_uri")]
    public string AuthUri { get; set; } = string.Empty;
    [ConfigurationKeyName("token_uri")]
    [JsonProperty("token_uri")]
    public string TokenUri { get; set; } = string.Empty;
    [ConfigurationKeyName("auth_provider_x509_cert_url")]
    [JsonProperty("auth_provider_x509_cert_url")]
    public string AuthProvider { get; set; } = string.Empty;
    [ConfigurationKeyName("client_x509_cert_url")]
    [JsonProperty("client_x509_cert_url")]
    public string ClientUri { get; set; } = string.Empty;
    [ConfigurationKeyName("universe_domain")]
    [JsonProperty("universe_domain")]
    public string UniverseDomain { get; set; } = string.Empty;
}