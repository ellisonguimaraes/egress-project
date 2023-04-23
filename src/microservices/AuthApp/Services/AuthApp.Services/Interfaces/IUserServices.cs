using AuthApp.Domain;

namespace AuthApp.Services.Interfaces;

public interface IUserServices
{
    Task RegisterAsync(User user, string password, bool isLockout = false);

    Task<Token> AuthenticateAsync(string email, string password);

    Task<Token> RefreshTokenAsync(string refreshToken);

    Task ConfirmEmailAsync(string email, string token);

    Task SendResetPasswordEmailAsync(string email);

    Task ResetPasswordAsync(string email, string token, string newPassword);

    Task<User> GetUserInfoAsync(string sub);

    Task ChangePasswordAsync(string id, string password, string newPassword);
}
