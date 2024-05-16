using Blog.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Blog.Core.Models.Comments; // Import the namespace for Comment DTOs

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

    [HttpPost]
    public async Task<IActionResult> CreateComment([FromBody] CreateCommentDto createCommentDto)
    {
        string jwtToken = GetJwtFromCookie_comments(); // Retrieve the JWT token from the cookie
        var createdComment = await _commentService.CreateComment(createCommentDto, jwtToken); // Pass both parameters
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
    private string GetJwtFromCookie_comments()
    {
        // Assuming the JWT token is stored in a cookie named 'AuthToken'
        var jwtCookie = HttpContext.Request.Cookies["AuthToken"];
        if (string.IsNullOrEmpty(jwtCookie))
        {
            throw new System.UnauthorizedAccessException("No token found in cookies.");
        }
        // Here you might want to add additional parsing if the cookie contains more than just the token

        return jwtCookie;
    }
}
