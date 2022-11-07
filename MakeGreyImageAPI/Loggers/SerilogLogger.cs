using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace MakeGreyImageAPI.Loggers;
/// <summary>
/// Class for logging with Serilog 
/// </summary>
public class SerilogLogger : Interfaces.ILogger
{
    private Logger _logger;
    
    /// <summary>
    /// SerilogLogger constructor
    /// </summary>
    public SerilogLogger()
    {
        _logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.File("Logg.txt", rollingInterval: RollingInterval.Day, rollOnFileSizeLimit: true)
            .CreateLogger();
    }
    /// <summary>
    /// Method for logging
    /// </summary>
    /// <param name="message">Message that we write</param>
    public void Log(string message)
    {
        _logger.Write(LogEventLevel.Debug, message);
    }
    /// <summary>
    /// Method for logging errors
    /// </summary>
    /// <param name="ex">Extension</param>
    /// <param name="message">Message</param>
    public void LogError(Exception ex, string message)
    {
        _logger.Write(LogEventLevel.Error, message);
    }
}