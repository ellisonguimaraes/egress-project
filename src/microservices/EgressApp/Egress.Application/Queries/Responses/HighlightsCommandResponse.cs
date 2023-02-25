using System.Text.Json.Serialization;

namespace Egress.Application.Queries.Responses;

public record HighlightsCommandResponse : BaseCommandResponse
{
    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("link")]
    public string? Link { get; set; }
}
