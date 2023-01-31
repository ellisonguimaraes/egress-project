using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Egress.Infra.CrossCutting.IoC;

/// <summary>
/// Logging extensions
/// </summary>
public static class ILoggingBuilderExtensions
{
    #region Constants
    const string APPLICATION_PROPERTY = "Application";
    const string APPLICATION_NAME = "ApplicationName";
    #endregion
    
    /// <summary>
    /// Serilog configuration
    /// </summary>
    /// <param name="logging">ILoggingBuilder object</param
    /// <param name="configuration">Configuration file ~ appsettings</param>
    public static void SerilogConfiguration(this ILoggingBuilder logging, IConfiguration configuration)
    {
        logging.ClearProviders();

        var logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .Enrich.WithProperty(APPLICATION_PROPERTY, configuration[APPLICATION_NAME]!)
            .CreateLogger();

        logging.AddSerilog(logger);
    }
}