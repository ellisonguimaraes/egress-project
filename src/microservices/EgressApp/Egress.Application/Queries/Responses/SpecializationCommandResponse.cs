using System.Text.Json.Serialization;

namespace Egress.Application.Queries.Responses;

public record SpecializationCommandResponse : BaseCommandResponse
{
    [JsonPropertyName("course_name")]
    public string CourseName { get; set; }

    [JsonPropertyName("institution_name")]
    public string InstitutionName { get; set; }

    [JsonPropertyName("status")]
    public bool Status { get; set; }

    [JsonPropertyName("modality")]
    public string Modality { get; set; }

    [JsonPropertyName("start_date")]
    public DateTime StartDate { get; set; }

    [JsonPropertyName("end_date")]
    public DateTime EndDate { get; set; }
}
