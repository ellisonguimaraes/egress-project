using System.Text.Json.Serialization;

namespace Egress.Application.Queries.Responses;

public record AddressCommandResponse : BaseCommandResponse
{
    [JsonPropertyName("street")]
    public string Street { get; set; }

    [JsonPropertyName("district")]
    public string District { get; set; }

    [JsonPropertyName("city")]
    public string City { get; set; }

    [JsonPropertyName("state")]
    public string State { get; set; }

    [JsonPropertyName("country")]
    public string Country { get; set; }
}
