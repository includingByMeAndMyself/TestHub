using Application.Dtos;
using Application.Topics;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller;

[Route("api/[controller]")]
[ApiController]
public class TopicsController(ITopicService topicService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<TopicResponseDto>>> GetTopics(CancellationToken ct)
    {
        return Ok(await topicService.GetTopicsAsync(ct));
    }
}