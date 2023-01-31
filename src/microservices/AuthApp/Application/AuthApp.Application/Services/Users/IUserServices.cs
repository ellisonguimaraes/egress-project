using AuthApp.Domain;

namespace AuthApp.Application.Services.Users;

public interface IUserServices
{
    Task RegisterAsync(User user, string password);

    Task<Token> AuthenticateAsync(string email, string password);

    Task<Token> RefreshTokenAsync(string refreshToken);

    Task ConfirmEmailAsync(string email, string token);

    Task RequestResetPasswordAsync(string email);

    Task ResetPasswordAsync(string email, string token, string newPassword);

    Task<User> GetUserInfoAsync(string sub);

    Task ChangePasswordAsync(string id, string password, string newPassword);
}
