using Application.Data.DataBaseContext;
using Application.Dtos;
using Application.Exceptions;
using Application.Extensions;
using Domain.Models;
using Domain.ValueObjects;
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
        catch (Exception exception)
        {
            throw;
        }
    }

    public async Task<TopicResponseDto> GetTopicAsync(Guid id, CancellationToken ct)
    {
        var topicId = TopicId.Of(id);
        var result = await dbContext
            .Topics
            .FindAsync([topicId], ct);

        if (result is null)
        {
            throw new TopicNotFoundException($"По {id} не найден topic");
        }

        return result.ToTopicResponseDto();
    }

    public async Task<TopicResponseDto> CreateTopicAsync(CreateTopicRequestDto dto, CancellationToken ct)
    {
        var newTopic = Topic.Create(
            TopicId.Of(Guid.NewGuid()),
            dto.Title,
            dto.EventStart,
            dto.Summary,
            dto.TopicType,
            Location.Of(dto.Location.City, dto.Location.Street)
        );

        await dbContext.Topics.AddAsync(newTopic, ct);
        await dbContext.SaveChangeAsync(ct);
        
        return newTopic.ToTopicResponseDto();
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