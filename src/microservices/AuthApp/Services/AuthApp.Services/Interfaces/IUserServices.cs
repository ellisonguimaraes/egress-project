using AuthApp.Domain;

namespace AuthApp.Services.Interfaces;

public interface IUserServices
{
    Task RegisterAsync(User user, string password);

    Task<Token> AuthenticateAsync(string email, string password);

    Task<Token> RefreshTokenAsync(string refreshToken);
}
