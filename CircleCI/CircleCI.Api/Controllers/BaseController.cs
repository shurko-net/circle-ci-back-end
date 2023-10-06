using AutoMapper;
using CircleCI.DataService.Repositories.Interfaces;
using CircleCI.Services.TokenService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CircleCI.Api.Controllers;

[Route("api")]
[ApiController]
[Authorize]
public class BaseController : ControllerBase
{
    protected readonly IUnitOfWork _unitOfWork;
    protected readonly IMapper _mapper;
    protected readonly IUserIdentifire _userIdentifire;
    public BaseController(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IUserIdentifire userIdentifire)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _userIdentifire = userIdentifire;
    }
}