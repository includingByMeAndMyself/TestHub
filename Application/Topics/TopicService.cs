using Application.Data.DataBaseContext;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Topics;

public class TopicService(
    IApplicationDbContext dbContext,
    ILogger<TopicService> logger)
    : ITopicService
{
    public async Task<List<Topic>> GetTopicsAsync()
    {
        var topic = await dbContext.Topics
            .AsNoTracking()
            .ToListAsync();

        return topic;
    }

    public Task<Topic> GetTopicAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Topic> CreateTopicAsync(Topic topicRequestDto)
    {
        throw new NotImplementedException();
    }

    public Task<Topic> UpdateTopicAsync(Guid id, Topic topicRequestDto)
    {
        throw new NotImplementedException();
    }

    public Task DeleteTopicAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}