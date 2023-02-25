using AuthApp.Application.Exceptions;
using AuthApp.Application.Models;
using AuthApp.Application.Services.Users;
using AuthApp.Domain;
using AuthApp.Infra.CrossCutting.Resources;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.Security.Claims;

namespace AuthApp.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserServices _userServices;
    private readonly IValidator<AuthRequest> _authRequestValidator;
    private readonly IValidator<RegisterRequest> _registerRequestValidator;
    private readonly IValidator<ResetPasswordRequest> _resetPasswordRequest;
    private readonly IValidator<ChangePasswordRequest> _changePasswordRequestValidator;
    private readonly IMapper _mapper;
    private readonly ILogger<UserController> _logger;

    public UserController(IUserServices userServices,
        IValidator<AuthRequest> authRequestValidator,
        IValidator<RegisterRequest> registerRequestValidator,
        IValidator<ResetPasswordRequest> resetPasswordRequest,
        IValidator<ChangePasswordRequest> changePasswordRequestValidator,
        IMapper mapper,
        ILogger<UserController> logger)
    {
        _userServices = userServices;
        _authRequestValidator = authRequestValidator;
        _registerRequestValidator = registerRequestValidator;
        _resetPasswordRequest = resetPasswordRequest;
        _changePasswordRequestValidator = changePasswordRequestValidator;
        _mapper = mapper;
        _logger = logger;
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequest registerRequest)
    {
        try
        {
            var result = _registerRequestValidator.Validate(registerRequest);

            if (!result.IsValid)
            {
                var response = Responses.WithError(StatusCodes.Status400BadRequest, result.Errors.Select(e => e.ErrorMessage));
                return StatusCode(response.StatusCode, response);
            }

            // Validar se usuário pertence ao banco. Se não, retornar informando.

            var user = _mapper.Map<User>(registerRequest);

            await _userServices.RegisterAsync(user, registerRequest.Password);

            return NoContent();
        }
        catch (AuthException ex)
        {
            var response = Responses.WithError(StatusCodes.Status400BadRequest, ex.Message);
            _logger.LogError(ex, $"{ex}, Email: {registerRequest.Email}");
            return StatusCode(response.StatusCode, response);
        }
        catch (Exception ex)
        {
            var response = Responses.WithError(StatusCodes.Status400BadRequest, ErrorCodeResource.UNEXPECTED_ERROR_OCURRED);
            _logger.LogError(ex, $"{ex}, {ErrorCodeResource.UNEXPECTED_ERROR_OCURRED}, TraceId: {Activity.Current?.Id}, Email: {registerRequest.Email}");
            return StatusCode(response.StatusCode, response);
        }
    }

    [HttpPost]
    [Route("authenticate")]
    public async Task<IActionResult> AuthenticateAsync([FromBody] AuthRequest authRequest)
    {
        try
        {
            var result = _authRequestValidator.Validate(authRequest);

            if (!result.IsValid)
            {
                var response = Responses.WithError(StatusCodes.Status400BadRequest, result.Errors.Select(e => e.ErrorMessage));
                return StatusCode(response.StatusCode, response);
            }
            else
            {
                var token = await _userServices.AuthenticateAsync(authRequest.Email, authRequest.Password);

                var response = Responses.WithSuccess(StatusCodes.Status200OK, _mapper.Map<TokenResponse>(token));
                return StatusCode(response.StatusCode, response);
            }
        }
        catch (AuthException ex)
        {
            var response = Responses.WithError(StatusCodes.Status400BadRequest, ex.Message);
            _logger.LogError(ex, $"{ex}, Email: {authRequest.Email}");
            return StatusCode(response.StatusCode, response);
        }
        catch (SecurityTokenException ex)
        {
            var response = Responses.WithError(StatusCodes.Status400BadRequest, ErrorCodeResource.ERROR_GENERATING_TOKENS);
            _logger.LogError(ex, $"{ex}, TraceId: {Activity.Current?.Id}, Email: {authRequest.Email}");
            return StatusCode(response.StatusCode, response);
        }
        catch (Exception ex)
        {
            var response = Responses.WithError(StatusCodes.Status400BadRequest, ErrorCodeResource.UNEXPECTED_ERROR_OCURRED);
            _logger.LogError(ex, $"{ex}, {ErrorCodeResource.UNEXPECTED_ERROR_OCURRED}, TraceId: {Activity.Current?.Id}, Email: {authRequest.Email}");
            return StatusCode(response.StatusCode, response);
        }
    }

    [HttpPost]
    [Route("refreshtoken")]
    public async Task<IActionResult> RefreshTokenAsync([FromHeader(Name = "refresh_token")] string refreshToken)
    {
        try
        {
            if (string.IsNullOrEmpty(refreshToken))
            {
                var response = Responses.WithError(StatusCodes.Status400BadRequest, ErrorCodeResource.REFRESH_TOKEN_HEADER_REQUIRED);
                return StatusCode(response.StatusCode, response);
            }
            else
            {
                var token = await _userServices.RefreshTokenAsync(refreshToken);

                var response = Responses.WithSuccess(StatusCodes.Status200OK, _mapper.Map<TokenResponse>(token));
                return StatusCode(response.StatusCode, response);
            }
        }
        catch (AuthException ex)
        {
            var response = Responses.WithError(StatusCodes.Status400BadRequest, ex.Message);
            _logger.LogError(ex, $"{ex}, RefreshToken: {refreshToken}");
            return StatusCode(response.StatusCode, response);
        }
        catch (SecurityTokenException ex)
        {
            var response = Responses.WithError(StatusCodes.Status400BadRequest, ErrorCodeResource.ERROR_GENERATING_TOKENS);
            _logger.LogError(ex, $"{ex}, TraceId: {Activity.Current?.Id}, RefreshToken: {refreshToken}");
            return StatusCode(response.StatusCode, response);
        }
        catch (Exception ex)
        {
            var response = Responses.WithError(StatusCodes.Status400BadRequest, ErrorCodeResource.UNEXPECTED_ERROR_OCURRED);
            _logger.LogError(ex, $"{ex}, {ErrorCodeResource.UNEXPECTED_ERROR_OCURRED}, TraceId: {Activity.Current?.Id}, RefreshToken: {refreshToken}");
            return StatusCode(response.StatusCode, response);
        }
    }

    [HttpGet]
    [Route("confirm_account")]
    public async Task<IActionResult> ConfirmEmailAsync([FromQuery(Name = "email")] string email, [FromQuery(Name = "token")] string token)
    {
        try
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(token))
            {
                var response = Responses.WithError(StatusCodes.Status400BadRequest, ErrorCodeResource.INVALID_PARAMETERS);
                return StatusCode(response.StatusCode, response);
            }

            await _userServices.ConfirmEmailAsync(email, token);

            return NoContent();
        }
        catch (AuthException ex)
        {
            var response = Responses.WithError(StatusCodes.Status400BadRequest, ex.Message);
            _logger.LogError(ex, $"{ex}, Email: {email}, Token: {token}");
            return StatusCode(response.StatusCode, response);
        }
        catch (Exception ex)
        {
            var response = Responses.WithError(StatusCodes.Status400BadRequest, ErrorCodeResource.UNEXPECTED_ERROR_OCURRED);
            _logger.LogError(ex, $"{ex}, {ErrorCodeResource.UNEXPECTED_ERROR_OCURRED}, TraceId: {Activity.Current?.Id}, Email: {email}, Token: {token}");
            return StatusCode(response.StatusCode, response);
        }
    }

    [HttpPost]
    [Route("password_reset")]
    public async Task<IActionResult> RequestResetPasswordAsync([FromHeader(Name = "email")] string email)
    {
        try
        {
            if (string.IsNullOrEmpty(email))
            {
                var response = Responses.WithError(StatusCodes.Status400BadRequest, ErrorCodeResource.EMAIL_NOT_INFORMED);
                return StatusCode(response.StatusCode, response);
            }

            await _userServices.RequestResetPasswordAsync(email);

            return NoContent();
        }
        catch (AuthException ex)
        {
            var response = Responses.WithError(StatusCodes.Status400BadRequest, ex.Message);
            _logger.LogError(ex, $"{ex}, Email: {email}");
            return StatusCode(response.StatusCode, response);
        }
        catch (Exception ex)
        {
            var response = Responses.WithError(StatusCodes.Status400BadRequest, ErrorCodeResource.UNEXPECTED_ERROR_OCURRED);
            _logger.LogError(ex, $"{ex}, {ErrorCodeResource.UNEXPECTED_ERROR_OCURRED}, TraceId: {Activity.Current?.Id}, Email: {email}");
            return StatusCode(response.StatusCode, response);
        }
    }

    [HttpPost]
    [Route("password_reset/{token}")]
    public async Task<IActionResult> ResetPasswordAsync([FromHeader(Name = "token")] string token, [FromBody] ResetPasswordRequest resetPasswordRequest)
    {
        try
        {
            var result = _resetPasswordRequest.Validate(resetPasswordRequest);

            if (!result.IsValid || token is null)
            {
                var response = Responses.WithError(StatusCodes.Status400BadRequest, result.Errors.Select(e => e.ErrorMessage));
                return StatusCode(response.StatusCode, response);
            }

            await _userServices.ResetPasswordAsync(resetPasswordRequest.Email, token, resetPasswordRequest.NewPassword);

            return NoContent();
        }
        catch (AuthException ex)
        {
            var response = Responses.WithError(StatusCodes.Status400BadRequest, ex.Message);
            _logger.LogError(ex, $"{ex}, Email: {resetPasswordRequest.Email}");
            return StatusCode(response.StatusCode, response);
        }
        catch (Exception ex)
        {
            var response = Responses.WithError(StatusCodes.Status400BadRequest, ErrorCodeResource.UNEXPECTED_ERROR_OCURRED);
            _logger.LogError(ex, $"{ex}, {ErrorCodeResource.UNEXPECTED_ERROR_OCURRED}, TraceId: {Activity.Current?.Id}, Email: {resetPasswordRequest.Email}");
            return StatusCode(response.StatusCode, response);
        }
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAsync()
    {
        try
        {
            var sub = HttpContext.User.Claims.SingleOrDefault(c => c.Type.Equals(ClaimTypes.NameIdentifier))?.Value;

            if (sub is null)
            {
                var response = Responses.WithError(StatusCodes.Status400BadRequest, ErrorCodeResource.INVALID_TOKEN_FORMAT);
                return StatusCode(response.StatusCode, response);
            }

            var result = await _userServices.GetUserInfoAsync(sub);

            var okResponse = Responses.WithSuccess(StatusCodes.Status200OK, _mapper.Map<UserResponse>(result));
            return StatusCode(okResponse.StatusCode, okResponse);
        }
        catch (AuthException ex)
        {
            var response = Responses.WithError(StatusCodes.Status400BadRequest, ex.Message);
            _logger.LogError(ex, ex.ToString());
            return StatusCode(response.StatusCode, response);
        }
        catch (Exception ex)
        {
            var response = Responses.WithError(StatusCodes.Status400BadRequest, ErrorCodeResource.UNEXPECTED_ERROR_OCURRED);
            _logger.LogError(ex, $"{ex}, {ErrorCodeResource.UNEXPECTED_ERROR_OCURRED}, TraceId: {Activity.Current?.Id}");
            return StatusCode(response.StatusCode, response);
        }
    }

    [HttpPost]
    [Route("password_restore")]
    [Authorize]
    public async Task<IActionResult> PasswordRestoreAsync([FromBody] ChangePasswordRequest changePasswordRequest)
    {
        try
        {
            var result = _changePasswordRequestValidator.Validate(changePasswordRequest);

            if (!result.IsValid)
            {
                var response = Responses.WithError(StatusCodes.Status400BadRequest, result.Errors.Select(e => e.ErrorMessage));
                return StatusCode(response.StatusCode, response);
            }

            var sub = HttpContext.User.Claims.SingleOrDefault(c => c.Type.Equals(ClaimTypes.NameIdentifier))!.Value;

            await _userServices.ChangePasswordAsync(sub, changePasswordRequest.Password, changePasswordRequest.NewPassword);

            return NoContent();
        }
        catch (AuthException ex)
        {
            var response = Responses.WithError(StatusCodes.Status400BadRequest, ex.Message);
            var sub = HttpContext.User.Claims.SingleOrDefault(c => c.Type.Equals(ClaimTypes.NameIdentifier))?.Value;
            _logger.LogError(ex, $"{ex}, Sub: {sub}");
            return StatusCode(response.StatusCode, response);
        }
        catch (Exception ex)
        {
            var response = Responses.WithError(StatusCodes.Status400BadRequest, ErrorCodeResource.UNEXPECTED_ERROR_OCURRED);
            var sub = HttpContext.User.Claims.SingleOrDefault(c => c.Type.Equals(ClaimTypes.NameIdentifier))?.Value;
            _logger.LogError(ex, $"{ex}, {ErrorCodeResource.UNEXPECTED_ERROR_OCURRED}, TraceId: {Activity.Current?.Id}, Sub: {sub}");
            return StatusCode(response.StatusCode, response);
        }
    }
}
