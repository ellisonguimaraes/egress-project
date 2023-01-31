using AuthApp.Domain;

namespace AuthApp.Application.Services.Jwt;

public interface IJwtServices
{
    string ValidateJwtToken(string token);

    Token GenerateToken(User user);
}
