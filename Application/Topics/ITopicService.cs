using Application.Dtos;
using Domain.Models;

namespace Application.Topics;

public interface ITopicService
{
    Task<List<TopicResponseDto>> GetTopicsAsync(CancellationToken ct);
    Task<TopicResponseDto> GetTopicAsync(Guid id, CancellationToken ct);
    Task<TopicResponseDto> CreateTopicAsync(CreateTopicRequestDto topicRequestDto, CancellationToken ct);
    Task<TopicResponseDto> UpdateTopicAsync(Guid id, UpdateTopicRequestDto topicRequestDto, CancellationToken ct);
    Task DeleteTopicAsync(Guid id, CancellationToken ct);
}