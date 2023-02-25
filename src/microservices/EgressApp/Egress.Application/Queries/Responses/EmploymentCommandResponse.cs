using System.Text.Json.Serialization;

namespace Egress.Application.Queries.Responses;

public record EmploymentCommandResponse : BaseCommandResponse
{
    [JsonPropertyName("role")]
    public string Role { get; set; }

    [JsonPropertyName("enterprise")]
    public string Enterprise { get; set; }

    [JsonPropertyName("section")]
    public string Section { get; set; }

    [JsonPropertyName("salary_range")]
    public decimal? SalaryRange { get; set; }

    [JsonPropertyName("initiative")]
    public string Initiative { get; set; }

    [JsonPropertyName("status")]
    public bool Status { get; set; }

    [JsonPropertyName("start_date")]
    public DateTime? StartDate { get; set; }

    [JsonPropertyName("end_date")]
    public DateTime? EndDate { get; set; }
}
