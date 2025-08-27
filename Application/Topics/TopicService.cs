using Application.Data.DataBaseContext;
using Application.Dtos;
using Application.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Topics;

public class TopicService(
    IApplicationDbContext dbContext,
    ILogger<TopicService> logger)
    : ITopicService
{
    public async Task<List<TopicResponseDto>> GetTopicsAsync(CancellationToken ct)
    {
        try
        {
            ct.ThrowIfCancellationRequested();
            
            var topic = await dbContext.Topics
                .AsNoTracking()
                .ToListAsync(ct);

            return topic.ToTopicResponseDtoList();
        }
        catch(Exception exception)
        {
            throw;
        }
    }

    public Task<TopicResponseDto> GetTopicAsync(Guid id, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<TopicResponseDto> CreateTopicAsync(CreateTopicRequestDto topicRequestDto, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task<TopicResponseDto> UpdateTopicAsync(Guid id, UpdateTopicRequestDto topicRequestDto, CancellationToken ct)
    {
        throw new NotImplementedException();
    }

    public Task DeleteTopicAsync(Guid id, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}