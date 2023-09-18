using AutoMapper;
using CircleCI.Api.Services.TokenService;
using CircleCI.DataService.Repositories.Interfaces;
using CircleCI.Entities.DbSet;
using CircleCI.Entities.DTOs.Requests;
using CircleCI.Entities.DTOs.Responses;
using Microsoft.AspNetCore.Mvc;

namespace CircleCI.Api.Controllers
{
    [Route("api")]
    [ApiController]
    public class CommentController : BaseController
    {
        public CommentController(IUnitOfWork unitOfWork,
            IMapper mapper,
            IUserIdentifire userIdentifire) : base(unitOfWork, mapper, userIdentifire)
        { }

        [HttpGet("get-comments/{postId}")]
        public async Task<IActionResult> GetComments(int postId)
        {
            var comments = await _unitOfWork.Comments.AllPostComments(postId);

            if (!comments.Any())
                return NotFound("Comments not found");
            
            return Ok(_mapper.Map<IEnumerable<GetCommentResponse>>(comments));
        }
        [HttpPost("save-comment")]
        public async Task<IActionResult> SaveComment([FromBody] CreateCommentRequest request)
        {
            request.UserId = _userIdentifire.GetIdByToken(Request);
            
            var comment = _mapper.Map<Comment>(request);

            await _unitOfWork.Comments.Add(comment);
            await _unitOfWork.CompleteAsync();
            
            return Ok(comment);
        }
    }
}
