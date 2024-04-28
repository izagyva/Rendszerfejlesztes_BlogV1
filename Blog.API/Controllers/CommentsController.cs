using Blog.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[Authorize]
[ApiController]
public class CommentsController : ControllerBase
{
    private readonly ICommentService _commentService;

    public CommentsController(ICommentService commentService)
    {
        _commentService = commentService;
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddComment(int topicId, string text)
    {
        await _commentService.AddComment(topicId, text);
        return Ok();
    }

    [HttpGet("topic/{id:int}")]
    public async Task<IActionResult> GetCommentsForTopic(int id)
    {
        var comments = await _commentService.GetCommentsForTopic(id);
        return Ok(comments);
    }
}