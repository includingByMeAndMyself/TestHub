namespace Application.Exceptions;

public class TopicNotFoundException(string message) : NotFoundException(message);