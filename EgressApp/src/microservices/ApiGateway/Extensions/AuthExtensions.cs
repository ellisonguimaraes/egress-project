using ApiGateway.Models.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ApiGateway.Extensions;

/// <summary>
/// IServiceCollection extensions to use Identity 
/// </summary>
public static class AuthExtensions
{
    #region CONSTANTS
    private const string JWT_SETTINGS_PROPERTY = "JwtSettings";

    private const string IDENTITY_SETTINGS_PASSWORD_REQUIRENONALPHANUMERIC_PROPERTY = "IdentitySetting:Password:RequireNonAlphanumeric";
    private const string IDENTITY_SETTINGS_PASSWORD_REQUIREDIGIT_PROPERTY = "IdentitySetting:Password:RequireDigit";
    private const string IDENTITY_SETTINGS_PASSWORD_REQUIREUPPERCASE_PROPERTY = "IdentitySetting:Password:RequireUppercase";
    private const string IDENTITY_SETTINGS_PASSWORD_REQUIRELOWERCASE_PROPERTY = "IdentitySetting:Password:RequireLowercase";
    private const string IDENTITY_SETTINGS_PASSWORD_REQUIRELENGTH_PROPERTY = "IdentitySetting:Password:RequiredLength";

    private const string IDENTITY_SETTINGS_SIGNIN_REQUIREUNIQUEEMAIL_PROPERTY = "IdentitySetting:SignIn:RequireUniqueEmail";

    private const string IDENTITY_SETTINGS_USER_REQUIRECONFIRMEDEMAIL_PROPERTY = "IdentitySetting:User:RequireConfirmedEmail";

    private const string IDENTITY_SETTINGS_LOCKOUT_ALLOWEDFORNEWUSERS_PROPERTY = "IdentitySetting:Lockout:AllowedForNewUsers";
    private const string IDENTITY_SETTINGS_LOCKOUT_DEFAULTLOCKOUTINSECONDS_PROPERTY = "IdentitySetting:Lockout:DefaultLockoutInSeconds";
    private const string IDENTITY_SETTINGS_LOCKOUT_MAXFAILEDACCESSATTEMPTS_PROPERTY = "IdentitySetting:Lockout:MaxFailedAccessAttempts";
    #endregion

    /// <summary>
    /// Allows the use of Identity in the API
    /// </summary>
    /// <param name="services">Service Collection</param>
    /// <param name="configuration">Configuration file (appsettings)</param>
    public static void AddAuthConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtSettings = configuration.GetSection(JWT_SETTINGS_PROPERTY).Get<JwtSettings>();

        // Authentication and Authorization settings
        services.AddAuthorization(options =>
        {
            //options.AddPolicy(RoleSettings.EGRESS, p => p.RequireAuthenticatedUser().RequireRole(RoleSettings.EGRESS).RequireClaim("ACTIVATED", "true"));
            //options.AddPolicy(RoleSettings.TEACHER, p => p.RequireAuthenticatedUser().RequireRole(RoleSettings.TEACHER).RequireClaim("ACTIVATED", "true"));
            //options.AddPolicy(RoleSettings.STUDENT, p => p.RequireAuthenticatedUser().RequireRole(RoleSettings.STUDENT).RequireClaim("ACTIVATED", "true"));
            //options.AddPolicy(RoleSettings.COLLEGIATE, p => p.RequireAuthenticatedUser().RequireRole(RoleSettings.COLLEGIATE).RequireClaim("ACTIVATED", "true").RequireClaim(RoleSettings.SECTION_CLAIM_NAME));
            //options.AddPolicy(RoleSettings.DEPARTMENT, p => p.RequireAuthenticatedUser().RequireRole(RoleSettings.DEPARTMENT).RequireClaim("ACTIVATED", "true").RequireClaim(RoleSettings.SECTION_CLAIM_NAME));
            //options.AddPolicy(RoleSettings.ADMINISTRATOR, p => p.RequireAuthenticatedUser().RequireRole(RoleSettings.ADMINISTRATOR).RequireClaim("ACTIVATED", "true"));
        });

        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateActor = jwtSettings.ValidateActor,
                ValidateAudience = jwtSettings.ValidateAudience,
                ValidateLifetime = jwtSettings.ValidateLifetime,
                ValidateIssuerSigningKey = jwtSettings.ValidateIssuerSigningKey,
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.SecretKey!))
            };
        });
    }
}
