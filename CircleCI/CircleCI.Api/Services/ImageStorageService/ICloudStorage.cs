namespace CircleCI.Api.Services.ImageStorageService;

public interface ICloudStorage
{
    Task<string> UploadFileAsync(IFormFile imageFile, string fileNameForStorage);
    Task DeleteFileAsync(string fileNameForStorage);
    string GetFileName();
}