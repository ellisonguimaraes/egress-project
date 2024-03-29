﻿using System.Text.Json;
using Egress.API.Models;
using Egress.Domain.Exceptions;
using Egress.Infra.CrossCutting.Resource;
using FluentValidation;

namespace Egress.API.Middlewares;

public class GlobalExceptionMiddleware
{
    #region Constants
    private const string CONTENT_TYPE = "application/json";
    private const string TRACE_ID_NAME = "TraceId";
    private const string ENDPOINT_NAME = "Endpoint";
    #endregion
    
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;
    
    public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }
    
    /// <summary>
    /// Exception interceptor method
    /// </summary>
    /// <param name="context">Http context</param>
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException e)
        {
            var response = new GenericHttpResponse
            {
                Errors = e.Errors.Select(error => error.ErrorMessage),
                Data = default
            };
            
            _logger.LogError(e, $"{LoggerResource.DATA_VALIDATION_FAILURE}, {ENDPOINT_NAME}: {context.Request.Path}, {TRACE_ID_NAME}: {response.TraceId}");

            await BuildResponseAsync(context, StatusCodes.Status400BadRequest, JsonSerializer.Serialize(response), CONTENT_TYPE);
        }
        catch (BusinessException e)
        {
            var response = new GenericHttpResponse
            {
                Errors = new List<string> { e.Message },
                Data = default
            };
            
            _logger.LogError(e, $"{LoggerResource.BUSINESS_FAILURE}: {e.Message}, {ENDPOINT_NAME}: {context.Request.Path}, {TRACE_ID_NAME}: {response.TraceId}");

            await BuildResponseAsync(context, StatusCodes.Status400BadRequest, JsonSerializer.Serialize(response), CONTENT_TYPE);
        }
        catch (Exception e)
        {
            var response = new GenericHttpResponse
            {
                Errors = new List<string> { ErrorCodeResource.UNEXPECTED_ERROR_OCURRED },
                Data = default
            };
            
            _logger.LogError(e, $"{LoggerResource.UNEXPECTED_ERROR_OCURRED}, {ENDPOINT_NAME}: {context.Request.Path}, {TRACE_ID_NAME}: {response.TraceId}");
            
            await BuildResponseAsync(context, StatusCodes.Status400BadRequest, JsonSerializer.Serialize(response), CONTENT_TYPE);
        }
    }

    /// <summary>
    /// Build HTTP response
    /// </summary>
    /// <param name="context">Context</param>
    /// <param name="statusCodes">Status code</param>
    /// <param name="body">Response body</param>
    /// <param name="contentType">Content type</param>
    private async Task BuildResponseAsync(HttpContext context, int statusCodes, string body, string contentType)
    {
        context.Response.Clear();
        context.Response.StatusCode = statusCodes;
        context.Response.ContentType = contentType;
        await context.Response.WriteAsync(body);
    }
}