﻿using AuthApp.Domain;
using AuthApp.Domain.Enums;
using AuthApp.Domain.Settings;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace AuthApp.Application.Services.Jwt;

/// <summary>
/// JwT Operations
/// </summary>
public class JwtServices : IJwtServices
{
    private readonly JwtSettings _jwtSettings;

    public JwtServices(JwtSettings jwtSettings)
    {
        _jwtSettings = jwtSettings;
    }

    /// <summary>
    /// Validate JWT token
    /// </summary>
    /// <param name="token">JwT Token</param>
    /// <returns>User id (sub claim value)</returns>
    public string ValidateJwtToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateActor = _jwtSettings.ValidateActor,
            ValidateAudience = _jwtSettings.ValidateAudience,
            ValidateLifetime = _jwtSettings.ValidateLifetime,
            ValidateIssuerSigningKey = _jwtSettings.ValidateIssuerSigningKey,
            ValidIssuer = _jwtSettings.Issuer,
            ValidAudience = _jwtSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.SecretKey!))
        };

        tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);

        var jwtToken = (JwtSecurityToken)validatedToken;

        var userId = jwtToken.Claims.First(x => x.Type.Equals(JwtRegisteredClaimNames.Sub)).Value;

        return userId;
    }

    /// <summary>
    /// Generate token payload
    /// </summary>
    /// <param name="user">User</param>
    /// <returns>Token Payload</returns>
    public Token GenerateToken(User user)
    {
        var accessTokenExpiresIn = DateTime.UtcNow.AddSeconds(_jwtSettings.ExpiryTimeInSeconds);
        var refreshTokenExpiresIn = DateTime.UtcNow.AddSeconds(_jwtSettings.RefreshTokenExpiryTimeInSeconds);

        var accessToken = GenerateAccessToken(user, accessTokenExpiresIn);
        var refreshToken = GenerateRefreshToken();

        var token = new Token
        {
            AccessToken = accessToken,
            AccessTokenExpiresIn = accessTokenExpiresIn,
            RefreshToken = refreshToken,
            RefreshTokenExpiresIn = refreshTokenExpiresIn,
            TokenType = TokenType.Bearer
        };

        return token;
    }

    /// <summary>
    /// Generate JWT access token
    /// </summary>
    /// <param name="user">User</param>
    /// <param name="expiresIn">Access token expiration date</param>
    /// <returns>JWT access token</returns>
    private string GenerateAccessToken(User user, DateTime expiresIn)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var key = Encoding.ASCII.GetBytes(_jwtSettings.SecretKey!);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email)
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = expiresIn,
            Audience = _jwtSettings.Audience,
            Issuer = _jwtSettings.Issuer,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    /// <summary>
    /// Generate refresh token
    /// </summary>
    /// <returns>Refresh token</returns>
    private string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
}
