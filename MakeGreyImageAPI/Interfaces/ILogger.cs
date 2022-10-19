namespace MakeGreyImageAPI.Interfaces;
/// <summary>
/// Logging interface
/// </summary>
public interface ILogger
{ 
    /// <summary>
    /// Log method
    /// </summary>
    /// <param name="message">message that we write</param>
    public void Log (string message);
    
}