using AuthApp.Application.Exceptions;
using AuthApp.Application.Extensions;
using AuthApp.Application.Services.Jwt;
using AuthApp.Application.Utils.EmailSender;
using AuthApp.Domain;
using AuthApp.Domain.Email;
using AuthApp.Domain.Enums;
using AuthApp.Infra.CrossCutting.Resources;
using AuthApp.Infra.Data.Repositories.RefreshToken;
using AuthApp.Services.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using MimeKit;
using AuthException = AuthApp.Application.Exceptions.AuthException;

namespace AuthApp.Application.Services.Users;

public class UserServices : IUserServices
{
    private const string URL_BASE_PROPERTY_NAME = "UrlBase";
    private const string API_DEFAULT_VERSION_PROPERTY_NAME = "ApiDefaultVersion";
    private const string DOT = ".";

    private readonly UserManager<User> _userManager;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IJwtServices _jwtServices;
    private readonly IEmailSender _emailSender;
    private readonly IConfiguration _configuration;
    private readonly string _urlBase;

    public UserServices(UserManager<User> userManager, IRefreshTokenRepository refreshTokenRepository, IJwtServices jwtServices, IEmailSender emailSender, IConfiguration configuration)
    {
        _userManager = userManager;
        _refreshTokenRepository = refreshTokenRepository;
        _jwtServices = jwtServices;
        _emailSender = emailSender;
        _configuration = configuration;
        _urlBase = $"{_configuration[URL_BASE_PROPERTY_NAME]}/api/v{_configuration[API_DEFAULT_VERSION_PROPERTY_NAME].Split(DOT)[0]}/user";
    }

    /// <summary>
    /// Register new user
    /// </summary>
    /// <param name="user">User</param>
    /// <param name="password">Password</param>
    public async Task RegisterAsync(User user, string password)
    {
        if (UserExists(user))
            throw new AuthException(ErrorCodeResource.REGISTER_USER_ALREADY_EXISTS);

        var result = await _userManager.CreateAsync(user, password);

        if (!result.Succeeded)
            throw new AuthException(ErrorCodeResource.REGISTER_COULD_NOT_REGISTER_USER, result);

        await AddToRoleAsync(user);

        var message = await BuildConfirmationEmailAsync(user);

        await _emailSender.SendAsync(message);
    }

    /// <summary>
    /// User exists verify
    /// </summary>
    /// <param name="user"></param>
    /// <returns>User exists or not</returns>
    private bool UserExists(User user) => _userManager.Users.Any(u =>
            u.UserName.Equals(user.UserName) ||
            u.Email.Equals(user.Email));

    /// <summary>
    /// Build confirmation email message
    /// </summary>
    /// <param name="user">User</param>
    /// <returns>MimeMessage</returns>
    private async Task<MimeMessage> BuildConfirmationEmailAsync(User user)
    {
        var emailToken = (await _userManager.GenerateEmailConfirmationTokenAsync(user)).Base64Encode();

        var url = $"{_urlBase}/confirm?token={emailToken}&email={user.Email}";

        var to = new List<To> { new To(user.Name, user.Email) };
        var body = string.Format(TemplatesResource.EMAIL_CONFIRM_MESSAGE, user.Name, url);
        var message = new Message(to, TemplatesResource.EMAIL_CONFIRM_SUBJECT, body);

        return _emailSender.CreateEmailMessage(message);
    }

    /// <summary>
    /// Add user role
    /// </summary>
    /// <param name="user">User</param>
    private async Task AddToRoleAsync(User user)
    {
        var getUser = await _userManager.FindByEmailAsync(user.Email);

        if (getUser is null)
            throw new AuthException(ErrorCodeResource.USER_NOT_FOUND);

        string permission = string.Empty;

        switch (getUser.UserType)
        {
            case UserType.STUDENT:
                permission = PermissionType.STUDENT.ToString();
                break;
            case UserType.EGRESS:
                permission = PermissionType.EGRESS.ToString();
                break;
            case UserType.TEACHER:
                permission = PermissionType.TEACHER.ToString();
                break;
        }

        var result = await _userManager.AddToRoleAsync(getUser, permission);

        if (!result.Succeeded)
            throw new AuthException(ErrorCodeResource.COULD_NOT_DEFINE_ROLE);
    }

    /// <summary>
    /// Authenticate User
    /// </summary>
    /// <param name="email">Email</param>
    /// <param name="password">Password</param>
    /// <returns>Token Payload</returns>
    public async Task<Token> AuthenticateAsync(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user is null)
            throw new AuthException(ErrorCodeResource.USER_NOT_FOUND);

        if (await _userManager.IsLockedOutAsync(user))
            throw new AuthException(ErrorCodeResource.USER_IS_BLOCKED);

        if (!await _userManager.CheckPasswordAsync(user, password))
        {
            await _userManager.AccessFailedAsync(user);
            throw new AuthException(ErrorCodeResource.INVALID_CREDENTIALS);
        }

        if (!await _userManager.IsEmailConfirmedAsync(user))
            throw new AuthException(ErrorCodeResource.CONFIRM_YOUR_ACCOUNT);

        await _userManager.ResetAccessFailedCountAsync(user);

        var token = await GenerateTokenAsync(user);

        return token;
    }

    /// <summary>
    /// Generete user token
    /// </summary>
    /// <param name="user">User</param>
    /// <returns>Token Payload</returns>
    private async Task<Token> GenerateTokenAsync(User user)
    {
        var token = _jwtServices.GenerateToken(user);

        var refreshTokenEntity = new RefreshToken
        {
            Id = Guid.NewGuid(),
            Token = token.RefreshToken,
            ExpiresIn = token.RefreshTokenExpiresIn,
            CreatedAt = DateTime.UtcNow,
            EditedAt = DateTime.UtcNow,
            IsActive = true,
            User = user
        };

        await _refreshTokenRepository.CreateAsync(refreshTokenEntity);

        return token;
    }

    /// <summary>
    /// Generate new token through RefreshToken
    /// </summary>
    /// <param name="refreshToken">RefreshToken</param>
    /// <returns>Token Payload</returns>
    public async Task<Token> RefreshTokenAsync(string refreshToken)
    {
        var refreshTokenEntity = _refreshTokenRepository.FindByToken(refreshToken);

        if (refreshTokenEntity is null || refreshTokenEntity.User is null)
            throw new AuthException(ErrorCodeResource.INVALID_REFRESH_TOKEN);

        if (await _userManager.IsLockedOutAsync(refreshTokenEntity.User))
            throw new AuthException(ErrorCodeResource.USER_IS_BLOCKED);

        if (!refreshTokenEntity.IsActive || refreshTokenEntity.ExpiresIn < DateTime.UtcNow)
            throw new AuthException(ErrorCodeResource.EXPIRED_REFRESH_TOKEN);

        await RevokeRefreshTokenAsync(refreshTokenEntity);

        var token = await GenerateTokenAsync(refreshTokenEntity.User);

        return token;
    }

    /// <summary>
    /// Revoke token: set IsActive = false
    /// </summary>
    /// <param name="refreshToken">Refresh Token Entity</param>
    private async Task RevokeRefreshTokenAsync(RefreshToken refreshToken)
    {
        refreshToken.IsActive = false;
        await _refreshTokenRepository.UpdateAsync(refreshToken);
    }

    /// <summary>
    /// Confirm user email
    /// </summary>
    /// <param name="email">Email</param>
    /// <param name="token">Confirmation token</param>
    public async Task ConfirmEmailAsync(string email, string token)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user is null)
            throw new AuthException(ErrorCodeResource.USER_NOT_FOUND);

        var result = await _userManager.ConfirmEmailAsync(user, token.Base64Decode());

        if (!result.Succeeded)
            throw new AuthException(ErrorCodeResource.COULD_NOT_CONFIRM, result);
    }

    /// <summary>
    /// Reset user password
    /// </summary>
    /// <param name="email">Email</param>
    public async Task RequestResetPasswordAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user is null)
            throw new AuthException(ErrorCodeResource.USER_NOT_FOUND);

        var message = await BuildResetPasswordEmailAsync(user);

        await _emailSender.SendAsync(message);
    }

    /// <summary>
    /// Build reset password email
    /// </summary>
    /// <param name="user">User</param>
    /// <returns>MimeMessage</returns>
    private async Task<MimeMessage> BuildResetPasswordEmailAsync(User user)
    {
        var emailToken = (await _userManager.GeneratePasswordResetTokenAsync(user)).Base64Encode();

        var url = $"{_urlBase}/resetpassword?token={emailToken}&email={user.Email}";

        var to = new List<To> { new To(user.Name, user.Email) };
        var body = string.Format(TemplatesResource.EMAIL_RESET_PASSWORD_MESSAGE, user.Name, url);
        var message = new Message(to, TemplatesResource.EMAIL_RESET_PASSWORD_SUBJECT, body);

        return _emailSender.CreateEmailMessage(message);
    }

    /// <summary>
    /// Reset user password
    /// </summary>
    /// <param name="email">Email</param>
    public async Task ResetPasswordAsync(string email, string token, string newPassword)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user is null)
            throw new AuthException(ErrorCodeResource.USER_NOT_FOUND);

        var result = await _userManager.ResetPasswordAsync(user, token.Base64Decode(), newPassword);

        if (!result.Succeeded)
            throw new AuthException(ErrorCodeResource.COULD_NOT_RESET_PASSWORD);

        await RevokeAllRefreshTokenAsync(user.Id);

        var message = BuildConfirmedResetPasswordEmail(user);

        await _emailSender.SendAsync(message);
    }

    /// <summary>
    /// Build reset password confirm email
    /// </summary>
    /// <param name="user">User</param>
    /// <returns>MimeMessage</returns>
    private MimeMessage BuildConfirmedResetPasswordEmail(User user)
    {
        var to = new List<To> { new To(user.Name, user.Email) };
        var body = string.Format(TemplatesResource.CONFIRMED_EMAIL_RESET_PASSWORD_MESSAGE, user.Name, _urlBase);
        var message = new Message(to, TemplatesResource.CONFIRMED_EMAIL_RESET_PASSWORD_SUBJECT, body);

        return _emailSender.CreateEmailMessage(message);
    }

    /// <summary>
    /// Get user infos
    /// </summary>
    /// <param name="sub">User id</param>
    /// <returns>User</returns>
    public async Task<User> GetUserInfoAsync(string sub)
    {
        var user = await _userManager.FindByIdAsync(sub);

        if (user is null)
            throw new AuthException(ErrorCodeResource.USER_NOT_FOUND);

        return user;
    }

    /// <summary>
    /// Change user password
    /// </summary>
    /// <param name="id">User id</param>
    /// <param name="password">Password</param>
    /// <param name="newPassword">New password</param>
    public async Task ChangePasswordAsync(string id, string password, string newPassword)
    {
        var user = await _userManager.FindByIdAsync(id);

        if (user is null)
            throw new AuthException(ErrorCodeResource.USER_NOT_FOUND);

        if (!await _userManager.CheckPasswordAsync(user, password))
            throw new AuthException(ErrorCodeResource.INVALID_PASSWORD);

        var result = await _userManager.ChangePasswordAsync(user, password, newPassword);

        if (!result.Succeeded)
            throw new AuthException(ErrorCodeResource.COULD_NOT_CHANGE_PASSWORD);

        await RevokeAllRefreshTokenAsync(user.Id);
    }

    /// <summary>
    /// Revoke all refresh token
    /// </summary>
    /// <param name="user">User</param>
    private async Task RevokeAllRefreshTokenAsync(string userId)
    {
        var refreshTokens = await _refreshTokenRepository.GetAllByUserIdAsync(userId);

        foreach (var refreshToken in refreshTokens)
            await RevokeRefreshTokenAsync(refreshToken);
    }
}