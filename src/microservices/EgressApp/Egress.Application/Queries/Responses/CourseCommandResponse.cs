using System.Text.Json.Serialization;

namespace Egress.Application.Queries.Responses;

public record CourseCommandResponse : BaseCommandResponse
{
    [JsonPropertyName("course_name")]
    public string CourseName { get; set; }
}
