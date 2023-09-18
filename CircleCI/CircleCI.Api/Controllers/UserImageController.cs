using AutoMapper;
using CircleCI.Api.Services.ImageStorageService;
using CircleCI.Api.Services.TokenService;
using CircleCI.DataService.Repositories.Interfaces;
using CircleCI.Entities.DTOs.Responses;
using Microsoft.AspNetCore.Mvc;

namespace CircleCI.Api.Controllers;

[Route("api")]
[ApiController]
public class UserImageController : BaseController
{
    private readonly ICloudStorage _cloudStorage;
    public UserImageController(IUnitOfWork unitOfWork,
        ICloudStorage cloudStorage,
        IMapper mapper,
        IUserIdentifire userIdentifire)
        : base(unitOfWork, mapper, userIdentifire)
    {
        _cloudStorage = cloudStorage;
    }

    [HttpGet("get-user-image")]
    public async Task<IActionResult> GetImage()
    {
        var user = await _unitOfWork.Users.GetByIdAsync(_userIdentifire.GetIdByToken(Request));

        if (user == null)
        {
            return Unauthorized();
        }
        
        return string.IsNullOrEmpty(user.ProfileImageUrl) ? 
            NotFound("Image not found") : 
            Ok(user.ProfileImageUrl);
    }
    
    [HttpPost("upload-user-image")]
    public async Task<IActionResult> AddImage(IFormFile file)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(_userIdentifire.GetIdByToken(Request));
        
        if (user == null)
        {
            return Unauthorized();
        }
        if (!string.IsNullOrEmpty(user.ProfileImageUrl))
        {
            await _cloudStorage.DeleteFileAsync(
                user.ProfileImageUrl.Substring(user.ProfileImageUrl.LastIndexOf('/') + 1));
        }
        
        user.ProfileImageUrl = await _cloudStorage.UploadFileAsync(file, _cloudStorage.GetFileName());
        await _unitOfWork.Users.UpdateAsync(user);
        await _unitOfWork.CompleteAsync();
        
        return Ok(_mapper.Map<UserAuthResponse>(user));
    }

    [HttpDelete("delete-user-image")]
    public async Task<IActionResult> DeleteImage()
    {
        var user = await _unitOfWork.Users.GetByIdAsync(_userIdentifire.GetIdByToken(Request));
        if (user == null)
        {
            return Unauthorized();
        }
        
        if (string.IsNullOrEmpty(user.ProfileImageUrl))
        {
            return NotFound("Image not found");
        }
        await _cloudStorage.DeleteFileAsync(
            user.ProfileImageUrl.Split('/').Last());
        user.ProfileImageUrl = string.Empty;
        await _unitOfWork.Users.UpdateAsync(user);
        await _unitOfWork.CompleteAsync();

        return NoContent();
    }
    [HttpGet("get-user-backimage")]
    public async Task<IActionResult> GetBackImage()
    {
        var user = await _unitOfWork.Users.GetByIdAsync(_userIdentifire.GetIdByToken(Request));

        if (user == null)
        {
            return Unauthorized();
        }
        
        return string.IsNullOrEmpty(user.BackgroundImageUrl) ? 
            NotFound("Image not found") : 
            Ok(user.BackgroundImageUrl);
    }
    
    [HttpPost("upload-user-backimage")]
    public async Task<IActionResult> AddBackImage(IFormFile file)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(_userIdentifire.GetIdByToken(Request));
        
        if (user == null)
        {
            return NotFound("User not found");
        }
        if (!string.IsNullOrEmpty(user.BackgroundImageUrl))
        {
            await _cloudStorage.DeleteFileAsync(
                user.BackgroundImageUrl.Substring(user.BackgroundImageUrl.LastIndexOf('/') + 1));
        }
        
        user.BackgroundImageUrl = await _cloudStorage.UploadFileAsync(file, _cloudStorage.GetFileName());
        await _unitOfWork.Users.UpdateAsync(user);
        await _unitOfWork.CompleteAsync();
        
        return Ok(_mapper.Map<UserAuthResponse>(user));
    }

    [HttpDelete("delete-user-backimage")]
    public async Task<IActionResult> DeleteBackImage()
    {
        var user = await _unitOfWork.Users.GetByIdAsync(_userIdentifire.GetIdByToken(Request));
        if (user == null)
        {
            return Unauthorized();
        }
        
        if (string.IsNullOrEmpty(user.BackgroundImageUrl))
        {
            return NotFound("Image not found");
        }
        
        await _cloudStorage.DeleteFileAsync(
            user.BackgroundImageUrl.Split('/').Last());
        user.BackgroundImageUrl = string.Empty;
        await _unitOfWork.Users.UpdateAsync(user);
        await _unitOfWork.CompleteAsync();

        return NoContent();
    }
}