using System.Text.Json.Serialization;

namespace Egress.Application.Queries.Responses;

public record PersonCommandResponse : BaseCommandResponse
{
    [JsonPropertyName("cpf")]
    public string Cpf { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("birth_date")]
    public string BirthDate { get; set; }

    [JsonPropertyName("sex")]
    public string Sex { get; set; }

    [JsonPropertyName("email")]
    public string Email { get; set; }

    [JsonPropertyName("phone_number")]
    public string PhoneNumber { get; set; }

    [JsonPropertyName("perfil_image")]
    public string PerfilImage { get; set; }

    [JsonPropertyName("expose_data")]
    public bool ExposeData { get; set; }

    [JsonPropertyName("person_type")]
    public string PersonType { get; set; }

    [JsonPropertyName("address")]
    public AddressCommandResponse Address { get; set; }

    [JsonPropertyName("courses")]
    public List<CourseCommandResponse> Courses { get; set; }

    [JsonPropertyName("employments")]
    public List<EmploymentCommandResponse> Employments { get; set; }

    [JsonPropertyName("highlights")]
    public List<HighlightsCommandResponse> Highlights { get; set; }

    [JsonPropertyName("specializations")]
    public List<SpecializationCommandResponse> Specializations { get; set; }

    [JsonPropertyName("testimonies")]
    public List<TestimonyCommandResponse> Testimonies { get; set; }
}
