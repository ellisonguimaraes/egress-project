using Egress.API.Middlewares;
using Egress.Infra.CrossCutting.IoC;
using Egress.Infra.Data.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

#region Constants
const string EGRESS_CONNECTION_STRING = "EgressDb";
const string HEALTH_CHECK_ROUTE = "/health";
#endregion

var builder = WebApplication.CreateBuilder(args);

// Logging Configuration
builder.Logging.SerilogConfiguration(builder.Configuration);

// Add services to the container.
builder.Services.AddControllers();

// EF Configuration
builder.Services.AddSqlServer<ApplicationDbContext>(builder.Configuration.GetConnectionString(EGRESS_CONNECTION_STRING)!);

// Versioning Configuration
builder.Services.AddApiVersioningConfiguration(builder.Configuration);

// Healt Check Configuration
builder.Services.AddHealthChecks();

// Clear .NET Built-in Validator (in the action inside the controller)
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services
    .AddAuthentication(
    options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

    })
    .AddJwtBearer(options =>
    {
        options.Audience = "https://portalegressos.uesc.br";
        options.Authority = "https://localhost:9001/";
        options.RequireHttpsMetadata = false;
        // options.TokenValidationParameters = new TokenValidationParameters
        // {
        //     ValidateIssuer = true,
        //     ValidateAudience = true,
        //     ValidateIssuerSigningKey = true,
        //     ValidAudiences = new[] {"https://portalegressos.uesc.br"},
        //     ValidIssuers = new[] { "AuthApp.Microservice" }
        // };
    });

// Register services
builder.Services.RegisterServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapHealthChecks(HEALTH_CHECK_ROUTE);

app.UseMiddleware<GlobalExceptionMiddleware>();

app.MapControllers();

app.Run();