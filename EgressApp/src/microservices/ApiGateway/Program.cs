using ApiGateway.Extensions;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

#region Constants
const string OCELOT_JSON_FILE_NAME = "ocelot.json";
#endregion

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Ocelot configuration
builder.Configuration.AddJsonFile(OCELOT_JSON_FILE_NAME);
builder.Services.AddOcelot(builder.Configuration);

// Authentication and Authorization configuration
builder.Services.AddAuthConfiguration(builder.Configuration);

var app = builder.Build();

await app.UseOcelot();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
