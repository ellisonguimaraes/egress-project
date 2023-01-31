using System.Text.Json.Serialization;
using MediatR;

namespace Egress.Application.Commands.Testimony;

public record CreateTestimonyCommand : IRequest<Guid>
{
    [JsonPropertyName("testimony")]
    public string Testimony { get; set; }
}