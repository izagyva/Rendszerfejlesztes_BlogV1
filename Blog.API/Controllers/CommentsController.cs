using Blog.Core.Services;
using Blog.Core.Constans;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Blog.Core.Models.Comments; // Import the namespace for Comment DTOs

[Route("api/[controller]")]
[Authorize(Roles = $"{Roles.User}")]
[ApiController]
public class CommentsController : ControllerBase
{
    private readonly ICommentService _commentService;

    public CommentsController(ICommentService commentService)
    {
        _commentService = commentService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateComment([FromBody] CreateCommentDto createCommentDto)
    {
        var createdComment = await _commentService.CreateComment(createCommentDto); // Pass both parameters
        return CreatedAtAction(nameof(GetComment), new { id = createdComment.Id }, createdComment);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetComment(int id)
    {
        var comment = await _commentService.GetComment(id);
        if (comment == null)
        {
            return NotFound();
        }
        return Ok(comment);
    }

    [HttpGet("topic/{topicId:int}")]
    public async Task<IActionResult> GetCommentsByTopic(int topicId, int pageNumber, int pageSize)
    {
        var comments = await _commentService.GetCommentsByTopic(topicId, pageNumber, pageSize);
        return Ok(comments);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateComment(int id, [FromBody] CreateCommentDto updateCommentDto)
    {
        var updatedComment = await _commentService.UpdateComment(id, updateCommentDto);
        return Ok(updatedComment);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteComment(int id)
    {
        await _commentService.DeleteComment(id);
        return NoContent();
    }

}
