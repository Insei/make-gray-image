using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace MakeGreyImageAPI.Loggers;
/// <summary>
/// Class for logging with Serilog 
/// </summary>
public class SerilogLogger : MakeGreyImageAPI.Interfaces.ILogger
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
    /// <param name="message">message that we write</param>
    public void Log(string message)
    {
        _logger.Write(LogEventLevel.Debug, message);
    }
}