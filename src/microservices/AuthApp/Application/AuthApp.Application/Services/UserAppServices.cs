using AuthApp.Application.Models;
using AuthApp.Application.Services.Interfaces;
using AuthApp.Application.Validators;
using AuthApp.Domain;
using AuthApp.Domain.Enums;
using AuthApp.Infra.CrossCutting.Resources;
using AuthApp.Services.Exceptions;
using AuthApp.Services.HttpClients;
using AuthApp.Services.Interfaces;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Refit;
using System;
using System.Diagnostics;
using System.Net;

namespace AuthApp.Application.Services;

public class UserAppServices : IUserAppServices
{
    #region Constants
    private const bool IS_LOCKOUT_IF_STUDENT_OR_TEACHER = true;
    #endregion

    private IUserServices _userServices;
    private IValidator<RegisterRequest> _registerRequestValidator;
    private readonly IValidator<AuthRequest> _authRequestValidator;
    private readonly IValidator<ResetPasswordRequest> _resetPasswordRequest;
    private readonly IValidator<ChangePasswordRequest> _changePasswordRequestValidator;
    private readonly IMapper _mapper;
    private readonly IEgressClient _egressClient;

    public UserAppServices(
        IUserServices userServices,
        IValidator<RegisterRequest> registerRequestValidator,
        IValidator<AuthRequest> authRequestValidator,
        IValidator<ResetPasswordRequest> resetPasswordRequest,
        IValidator<ChangePasswordRequest> changePasswordRequestValidator,
        IMapper mapper,
        IEgressClient egressClient)
    {
        _userServices = userServices;
        _registerRequestValidator = registerRequestValidator;
        _authRequestValidator = authRequestValidator;
        _resetPasswordRequest = resetPasswordRequest;
        _changePasswordRequestValidator = changePasswordRequestValidator;
        _mapper = mapper;
        _egressClient = egressClient;
    }

    /// <summary>
    /// Register new user
    /// </summary>
    /// <param name="registerRequest">Registration data</param>
    /// <returns></returns>
    public async Task RegisterAsync(RegisterRequest registerRequest)
    {
        _registerRequestValidator.Validate(registerRequest, opt => opt.ThrowOnFailures());

        var user = BuildUser(registerRequest);

        if (registerRequest.UserType.Equals(UserType.STUDENT) || registerRequest.UserType.Equals(UserType.TEACHER))
        {
            await _userServices.RegisterAsync(user, registerRequest.Password, IS_LOCKOUT_IF_STUDENT_OR_TEACHER);
        }
        else if (registerRequest.UserType.Equals(UserType.EGRESS))
        {
            user.PersonId = await GetPersonIdAsync(registerRequest.Document, registerRequest.DocumentType);

            await _userServices.RegisterAsync(user, registerRequest.Password);
        }
    }

    /// <summary>
    /// Build user. Convert RegisterRequest into User
    /// </summary>
    /// <param name="registerRequest">Registration data</param>
    /// <returns>User</returns>
    private User BuildUser(RegisterRequest registerRequest)
    {
        var user = _mapper.Map<User>(registerRequest);

        user.CreatedAt = DateTime.UtcNow;
        user.EditedAt = user.CreatedAt;

        return user;
    }

    /// <summary>
    /// Identify egress in egress_api
    /// </summary>
    /// <param name="document">Document number</param>
    /// <param name="documentType">Document type</param>
    /// <returns></returns>
    /// <exception cref="BusinessException"></exception>
    private async Task<Guid> GetPersonIdAsync(string document, DocumentType documentType)
    {
        var response = await _egressClient.GetPersonByDocumentAsync(document, ((byte)documentType));

        if (!response.IsSuccessStatusCode)
        {
            throw new BusinessException(ErrorCodeResource.NOT_IDENTITY_EGRESS);
        }

        var person = response.Content.Data;

        return person!.Id;
    }

    /// <summary>
    /// Authenticate user
    /// </summary>
    /// <param name="authRequest">Email and password</param>
    /// <returns>Token</returns>
    public async Task<TokenResponse> AuthenticateAsync(AuthRequest authRequest)
    {
        _authRequestValidator.Validate(authRequest, opt => opt.ThrowOnFailures());

        var token = await _userServices.AuthenticateAsync(authRequest.Email, authRequest.Password);

        return _mapper.Map<TokenResponse>(token);
    }

    /// <summary>
    /// Authenticate user by refresh token
    /// </summary>
    /// <param name="refreshToken">Refresh token</param>
    /// <returns>New token</returns>
    public async Task<TokenResponse> RefreshTokenAsync(string refreshToken)
    {
        if (string.IsNullOrEmpty(refreshToken))
            throw new BusinessException(ErrorCodeResource.REFRESH_TOKEN_HEADER_REQUIRED);

        var token = await _userServices.RefreshTokenAsync(refreshToken);

        return _mapper.Map<TokenResponse>(token);
    }

    /// <summary>
    /// Confirm email
    /// </summary>
    /// <param name="email">Email</param>
    /// <param name="token">Confirmation token</param>
    public async Task ConfirmEmailAsync(string email, string token)
    {
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(token))
            throw new BusinessException(ErrorCodeResource.INVALID_PARAMETERS);

        await _userServices.ConfirmEmailAsync(email, token);
    }

    /// <summary>
    /// Send reset password email
    /// </summary>
    /// <param name="email">Email</param>
    public async Task SendResetPasswordEmailAsync(string email)
    {
        if (string.IsNullOrEmpty(email))
            throw new BusinessException(ErrorCodeResource.EMAIL_NOT_INFORMED);

        await _userServices.SendResetPasswordEmailAsync(email);
    }

    /// <summary>
    /// Reset password
    /// </summary>
    /// <param name="resetPasswordRequest">Token, email and new password</param>
    public async Task ResetPasswordAsync(ResetPasswordRequest resetPasswordRequest)
    {
        _resetPasswordRequest.Validate(resetPasswordRequest, opt => opt.ThrowOnFailures());

        await _userServices.ResetPasswordAsync(resetPasswordRequest.Email, resetPasswordRequest.Token, resetPasswordRequest.NewPassword);
    }

    /// <summary>
    /// Change password
    /// </summary>
    /// <param name="sub">User id</param>
    /// <param name="changePasswordRequest">Old password, new password and repeat new password</param>
    public async Task ChangePasswordAsync(string? sub, ChangePasswordRequest changePasswordRequest)
    {
        _changePasswordRequestValidator.Validate(changePasswordRequest, opt => opt.ThrowOnFailures());

        if (string.IsNullOrEmpty(sub))
            throw new AuthException(ErrorCodeResource.INVALID_AUTHORIZATION_TOKEN);

        await _userServices.ChangePasswordAsync(sub, changePasswordRequest.Password, changePasswordRequest.NewPassword);
    }

    /// <summary>
    /// Get user info
    /// </summary>
    /// <param name="sub">User id</param>
    /// <returns>User data</returns>
    public async Task<UserResponse> GetUserInfoAsync(string? sub)
    {
        if (string.IsNullOrEmpty(sub))
            throw new AuthException(ErrorCodeResource.INVALID_AUTHORIZATION_TOKEN);

        var result = await _userServices.GetUserInfoAsync(sub);

        return _mapper.Map<UserResponse>(result);
    }
}

