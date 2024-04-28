using Blog.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[Route("api/[controller]")]
[Authorize]
[ApiController]
public class TopicsController : ControllerBase
{
    private readonly ITopicService _topicService;

    public TopicsController(ITopicService topicService)
    {
        _topicService = topicService;
    }

    [HttpGet]
    public async Task<IActionResult> GetTopics(int pageNumber, int pageSize)
    {
        var topics = await _topicService.GetTopics(pageNumber, pageSize);
        return Ok(topics);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetTopic(int id)
    {
        var topic = await _topicService.GetTopic(id);
        if (topic == null)
        {
            return NotFound();
        }
        return Ok(topic);
    }

    [HttpGet("user")]
    public async Task<IActionResult> GetUserTopics()
    {
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (int.TryParse(userIdString, out int userId))
        {
            var topics = await _topicService.GetTopicsByUser(userId);
            return Ok(topics);
        }
        else
        {
            // Handle the case when userIdString is not a valid integer.
            // You might want to return a BadRequest result or throw an exception.
            return BadRequest("User ID is not a valid integer.");
        }
    }
}