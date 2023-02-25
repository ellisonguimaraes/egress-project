using System.Text.Json.Serialization;

namespace Egress.Application.Queries.Responses;

public record TestimonyCommandResponse : BaseCommandResponse
{
    [JsonPropertyName("content")]
    public string Content { get; set; }

    [JsonPropertyName("was_accepted")]
    public bool WasAccepted { get; set; }
}
