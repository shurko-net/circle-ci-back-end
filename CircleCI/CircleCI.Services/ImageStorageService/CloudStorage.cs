using CircleCI.Services.Configuration;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace CircleCI.Services.ImageStorageService;

public class CloudStorage : ICloudStorage
{
    private readonly StorageClient _storageClient;
    private readonly string _bucketName;

    public CloudStorage(IOptions<GoogleConfig> googleConfig)
    {
        var googleConfiguration = googleConfig.Value;
        var json = JsonConvert.SerializeObject(googleConfiguration.GoogleBucket.GoogleCredential);
        var googleCredential = GoogleCredential.FromJson(json);
        _storageClient = StorageClient.Create(googleCredential);
        _bucketName = googleConfiguration.GoogleBucket.GoogleCloudStorageBucket;
    }
    
    public async Task<string> UploadFileAsync(IFormFile imageFile, string fileNameForStorage)
    {
        using var memoryStream = new MemoryStream();
        await imageFile.CopyToAsync(memoryStream);

        await _storageClient.UploadObjectAsync(_bucketName, fileNameForStorage, "image/png", memoryStream);
            
        return $"https://storage.cloud.google.com/{_bucketName}/{fileNameForStorage}";
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