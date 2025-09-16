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
        ct.ThrowIfCancellationRequested();

        var topic = await dbContext.Topics
            .AsNoTracking()
            .Where(t => t.IsDeleted == false)
            .ToListAsync(ct);

        return topic.ToTopicResponseDtoList();
    }

    public async Task<TopicResponseDto> GetTopicAsync(Guid id, CancellationToken ct)
    {
        var topicId = TopicId.Of(id);
        var result = await dbContext
            .Topics
            .FindAsync([topicId], ct);

        if (result is null || result.IsDeleted)
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
        await dbContext.SaveChangesAsync(ct);

        return newTopic.ToTopicResponseDto();
    }

    public async Task<TopicResponseDto> UpdateTopicAsync(Guid id, UpdateTopicRequestDto dto, CancellationToken ct)
    {
        var topicId = TopicId.Of(id);

        var topic = await dbContext.Topics.FindAsync([topicId], ct);

        if (topic is null || topic.IsDeleted)
        {
            throw new TopicNotFoundException($"По {id} не найден topic");
        }

        topic.Title = dto.Title ?? topic.Title;
        topic.Summary = dto.Summary ?? topic.Summary;
        topic.TopicType = dto.TopicType ?? topic.TopicType;
        topic.EventStart = dto.EventStart;
        topic.Location = Location.Of(
            dto.Location.City,
            dto.Location.Street) ?? topic.Location;

        await dbContext.SaveChangesAsync(ct);

        return topic.ToTopicResponseDto();
    }

    public async Task DeleteTopicAsync(Guid id, CancellationToken ct)
    {
        var topicId = TopicId.Of(id);
        
        var topic = await dbContext.Topics.FindAsync([topicId], ct);
        
        if (topic is null || topic.IsDeleted)
        {
            throw new TopicNotFoundException($"По {id} не найден topic");
        }

        topic.IsDeleted = true;
        topic.DeletedAt = DateTimeOffset.UtcNow;
        
        //dbContext.Topics.Remove(topic);
        await dbContext.SaveChangesAsync(ct);
    }
}