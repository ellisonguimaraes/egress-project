using System.Text.Json.Serialization;

namespace AuthApp.Application.Models;

public class ResetPasswordRequest
{
    [JsonPropertyName("email")]
    public string Email { get; set; }

    [JsonPropertyName("new_password")]
    public string NewPassword { get; set; }
}
