using CircleCI.Api.Configuration;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Microsoft.Extensions.Options;

namespace CircleCI.Api.Services.ImageStorageService;

public class CloudStorage : ICloudStorage
{
    private readonly StorageClient _storageClient;
    private readonly string _bucketName;

    public CloudStorage(IOptions<GoogleConfig> googleConfig)
    {
        var googleConfiguration = googleConfig.Value;
        var googleCredential = GoogleCredential.FromFile(googleConfiguration.GoogleCredentialFile);
        _storageClient = StorageClient.Create(googleCredential);
        _bucketName = googleConfiguration.GoogleCloudStorageBucket;
    }
    
    public async Task<string> UploadFileAsync(IFormFile imageFile, string fileNameForStorage)
    {
        using (var memoryStream = new MemoryStream())
        {
            await imageFile.CopyToAsync(memoryStream);

            await _storageClient.UploadObjectAsync(_bucketName, fileNameForStorage, "image/png", memoryStream);
            
            return $"https://storage.cloud.google.com/{_bucketName}/{fileNameForStorage}";
        }
    }

    public async Task DeleteFileAsync(string fileNameForStorage)
    {
        await _storageClient.DeleteObjectAsync(_bucketName, fileNameForStorage);
    }

    public string GetFileName()
    {
        return $"{Guid.NewGuid()}_{DateTime.UtcNow.ToShortDateString()}.png";
    }
}