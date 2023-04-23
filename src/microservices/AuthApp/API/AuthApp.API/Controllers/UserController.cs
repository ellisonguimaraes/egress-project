﻿using AuthApp.Application.Models;
using AuthApp.Services.Interfaces;
using AuthApp.Domain;
using AuthApp.Infra.CrossCutting.Resources;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.Security.Claims;
using AuthApp.Application.Services.Interfaces;
using AuthApp.Services.Exceptions;
using AuthApp.Domain.Utils;

namespace AuthApp.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserAppServices _userAppServices;

    public UserController(IUserAppServices userAppServices)
    {
        _userAppServices = userAppServices;
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequest registerRequest)
    {
        await _userAppServices.RegisterAsync(registerRequest);
        return NoContent();
    }

    [HttpPost]
    [Route("authenticate")]
    public async Task<IActionResult> AuthenticateAsync([FromBody] AuthRequest authRequest)
    {
        var result = await _userAppServices.AuthenticateAsync(authRequest);
        return Ok(new GenericHttpResponse<TokenResponse> { Data = result });
    }

    [HttpPost]
    [Route("refreshtoken")]
    public async Task<IActionResult> RefreshTokenAsync([FromHeader(Name = "refresh_token")] string refreshToken)
    {
        var result = await _userAppServices.RefreshTokenAsync(refreshToken);
        return Ok(new GenericHttpResponse<TokenResponse> { Data = result });
    }

    [HttpGet]
    [Route("confirm_account")]
    public async Task<IActionResult> ConfirmEmailAsync([FromQuery(Name = "email")] string email, [FromQuery(Name = "token")] string token)
    {
        await _userAppServices.ConfirmEmailAsync(email, token);
        return NoContent();
    }

    [HttpPost]
    [Route("password_reset/send")]
    public async Task<IActionResult> SendResetPasswordEmailAsync([FromHeader(Name = "email")] string email)
    {
        await _userAppServices.SendResetPasswordEmailAsync(email);
        return NoContent();
    }

    [HttpPost]
    [Route("password_reset")]
    public async Task<IActionResult> ResetPasswordAsync([FromBody] ResetPasswordRequest resetPasswordRequest)
    {
        await _userAppServices.ResetPasswordAsync(resetPasswordRequest);
        return NoContent();
    }

    [HttpPost]
    [Route("change_password")]
    [Authorize]
    public async Task<IActionResult> ChangePasswordAsync([FromBody] ChangePasswordRequest changePasswordRequest)
    {
        var sub = HttpContext.User.Claims.SingleOrDefault(c => c.Type.Equals(ClaimTypes.NameIdentifier))?.Value;
        await _userAppServices.ChangePasswordAsync(sub, changePasswordRequest);
        return NoContent();
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAsync()
    {
        var sub = HttpContext.User.Claims.SingleOrDefault(c => c.Type.Equals(ClaimTypes.NameIdentifier))?.Value;
        var result = await _userAppServices.GetUserInfoAsync(sub);
        return Ok(result);
    }
}
