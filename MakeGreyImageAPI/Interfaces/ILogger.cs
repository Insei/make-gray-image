namespace MakeGreyImageAPI.Interfaces;
/// <summary>
/// Logging interface
/// </summary>
public interface ILogger
{ 
    /// <summary>
    /// Log method
    /// </summary>
    /// <param name="message">Message that we write</param>
    public void Log (string message);
    /// <summary>
    /// Log for errors method
    /// </summary>
    /// <param name="ex">Extension</param>
    /// <param name="message">Message</param>
    void LogError(Exception ex, string message);
}