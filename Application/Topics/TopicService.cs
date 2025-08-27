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
    public async Task<List<Topic>> GetTopicsAsync(CancellationToken ct)
    {
        try
        {
            ct.ThrowIfCancellationRequested();
            
            var topic = await dbContext.Topics
                .AsNoTracking()
                .ToListAsync(ct);

            return topic;
        }
        catch(Exception exception)
        {
            throw;
        }
    }

    public Task<Topic> GetTopicAsync(Guid id, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<Topic> CreateTopicAsync(Topic topicRequestDto, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<Topic> UpdateTopicAsync(Guid id, Topic topicRequestDto, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task DeleteTopicAsync(Guid id, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}