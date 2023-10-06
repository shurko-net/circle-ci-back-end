using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace CircleCI.Services.Configuration;

public class GoogleConfig
{
    [JsonProperty("GoogleBucket")]
    [ConfigurationKeyName("GoogleBucket")]
    public GoogleBucket GoogleBucket { get; set; } = new();
}

public class GoogleBucket
{
    [JsonProperty("GoogleCredential")]
    public GoogleCred GoogleCredential { get; set; } = new();

    [JsonProperty("GoogleCloudStorageBucket")]
    public string GoogleCloudStorageBucket { get; set; } = string.Empty;
}

public class GoogleCred
{
    [JsonProperty("type")]
    public string Type { get; set; }  = string.Empty;

    [JsonProperty("project_id")]
    public string ProjectId { get; set; } = string.Empty;

    [JsonProperty("private_key_id")]
    public string PrivateKeyId { get; set; } = string.Empty;

    [JsonProperty("private_key")]
    public string PrivateKey { get; set; } = string.Empty;

    [JsonProperty("client_email")]
    public string ClientEmail { get; set; } = string.Empty;

    [JsonProperty("client_id")]
    public string ClientId { get; set; } = string.Empty;

    [JsonProperty("auth_uri")]
    public string AuthUri { get; set; } = string.Empty;

    [JsonProperty("token_uri")]
    public string TokenUri { get; set; } = string.Empty;

    [JsonProperty("auth_provider_x509_cert_url")]
    public string AuthProviderX509CertUrl { get; set; } = string.Empty;

    [JsonProperty("client_x509_cert_url")]
    public string ClientX509CertUrl { get; set; } = string.Empty;

    [JsonProperty("universe_domain")]
    public string UniverseDomain { get; set; } = string.Empty;
}