using AuthApp.Application.Mapper;
using AuthApp.Application.Models;
using AuthApp.Application.Services.Jwt;
using AuthApp.Application.Services.Users;
using AuthApp.Application.Utils.EmailSender;
using AuthApp.Application.Validators;
using AuthApp.Domain.Settings;
using AuthApp.Infra.Data.Repositories.RefreshToken;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AuthApp.Infra.CrossCutting.IoC;

/// <summary>
/// IServiceCollection extension to register services
/// </summary>
public static class DependencyInjectionExtensions
{
    private const string EMAIL_SETTINGS_PROPERTY_NAME = "EmailSettings";

    /// <summary>
    /// Register services
    /// </summary>
    /// <param name="services">Service Collection</param>
    /// <param name="configuration">Configuration file (appsettings)</param>
    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Settings
        services.Configure<EmailSettings>(configuration.GetSection(EMAIL_SETTINGS_PROPERTY_NAME));

        // Services
        services.AddScoped<IEmailSender, EmailSender>();
        services.AddScoped<IJwtServices, JwtServices>();
        services.AddScoped<IUserServices, UserServices>();

        // Repositories
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();

        // Validators
        services.AddScoped<IValidator<AuthRequest>, AuthRequestValidator>();
        services.AddScoped<IValidator<RegisterRequest>, RegisterRequestValidator>();
        services.AddScoped<IValidator<ResetPasswordRequest>, ResetPasswordRequestValidator>();
        services.AddScoped<IValidator<ChangePasswordRequest>, ChangePasswordRequestValidator>();

        // Mapper
        services.AddAutoMapper(typeof(TokenProfile));
        services.AddAutoMapper(typeof(UserProfile));
    }
}
