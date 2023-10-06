using Google.Cloud.SecretManager.V1;
using Npgsql;

namespace CircleCI.Services.GoogleSecrets;

public static class SecretManager
{
    private const string ProjectId = "circleci-388910";
    private const string ConnectionStringSecret = "circleci_connection_string";
    private const string BucketCredentialSecret = "bucket_credentials";
    private const string JwtConfigurationSecret = "circleci_jwt_secrets";
    public static async Task<string> GetConnectionString()
    {
        var response = await GetSecretVersionResponse(ConnectionStringSecret, "3");

        return response.Payload.Data.ToStringUtf8();
    }
    public static async Task<string> GetBucketCredentials()
    {
        var response = await GetSecretVersionResponse(BucketCredentialSecret);

        return response.Payload.Data.ToStringUtf8();
    }
    public static async Task<string> GetJwtConfiguration()
    {
        var response = await GetSecretVersionResponse(JwtConfigurationSecret);

        return response.Payload.Data.ToStringUtf8();
    }
    private static async Task<AccessSecretVersionResponse> GetSecretVersionResponse(string secretString, string version = "latest")
    {
        var client = await SecretManagerServiceClient.CreateAsync();
        var secretVersion = new SecretVersionName(ProjectId, secretString, version);
        var response = await client.AccessSecretVersionAsync(secretVersion);

        return response;
    }
}