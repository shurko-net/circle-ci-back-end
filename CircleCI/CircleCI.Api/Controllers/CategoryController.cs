using AutoMapper;
using CircleCI.Api.Services.TokenService;
using CircleCI.DataService.Repositories.Interfaces;
using CircleCI.Entities.DbSet;
using CircleCI.Entities.DTOs.Requests;
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

    [HttpPost("create-categories")]
    public async Task<IActionResult> CreateCategories(IEnumerable<CreateCategoryRequest> categories)
    {
        List<Category> temp = categories
            .SelectMany(n => n.CategoryNames.Select(name => new Category
            {
                PostId = n.PostId,
                Name = name
            })).ToList();

        await _unitOfWork.Categories.AddRange(temp);
        await _unitOfWork.CompleteAsync();
        
        return Ok(temp);
    }
}