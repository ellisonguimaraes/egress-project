using AuthApp.Domain;

namespace AuthApp.Services.Interfaces;

public interface IJwtServices
{
    string ValidateJwtToken(string token);

    Token GenerateToken(User user);
}
