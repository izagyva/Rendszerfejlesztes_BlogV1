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
    private string GetJwtFromCookie_topics()
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
