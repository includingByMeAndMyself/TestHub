using Application.Topics;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller;

[Route("api/[controller]")]
[ApiController]
public class TopicsController(ITopicService topicService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<Topic>>> GetTopics(CancellationToken ct)
    {
        return Ok(await topicService.GetTopicsAsync(ct));
    }
}