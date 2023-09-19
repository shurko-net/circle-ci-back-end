using AutoMapper;
using CircleCI.Api.Services.TokenService;
using CircleCI.DataService.Repositories.Interfaces;
using CircleCI.Entities.DbSet;
using CircleCI.Entities.DTOs.Requests;
using CircleCI.Entities.DTOs.Responses;
using Microsoft.AspNetCore.Mvc;

namespace CircleCI.Api.Controllers;

[Route("api")]
[ApiController]
public class CategoryController : BaseController
{
    public CategoryController(IUnitOfWork unitOfWork,
        IMapper mapper,
        IUserIdentifire userIdentifire) : base(unitOfWork, mapper, userIdentifire)
    { }

    [HttpGet("tag-search/{query?}")]
    public async Task<IActionResult> TagSearch(string query = "")
    {
        if (string.IsNullOrEmpty(query))
        {
            return NotFound("Empty query string");
        }

        var list = await _unitOfWork.CategoriesList.SearchTagsAsync(query);

        if (!list.Any())
        {
            return NotFound("Tags not found");
        }
        
        return Ok(_mapper.Map<IEnumerable<GetTagsResponse>>(list));
    }
}