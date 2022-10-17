namespace MakeGreyImageAPI.Entities;
/// <summary>
/// a class containing the request execution status
/// </summary>
public class ResultStatus
{
    /// <summary>
    /// boolean value about the execution of the request
    /// </summary>
    public bool Succeeded { get; set; }
    /// <summary>
    /// list of text messages about the request status
    /// </summary>
    public List<string> Messages { get; set; }
    /// <summary>
    /// ResultStatus class constructor
    /// </summary>
    public ResultStatus()
    {
        Succeeded = true;
        Messages = new List<string>();
    }
}
/// <summary>
/// a class containing an instance field of the ResultStatus 
/// </summary>
public class Result
{
    /// <summary>
    /// instance of the ResultStatus
    /// </summary>
    public ResultStatus Status { get; set; }
    
    /// <summary>
    /// Result constructor
    /// </summary>
    public Result()
    {
        Status = new ResultStatus();
    }
}
/// <summary>
/// a class containing an instance field of the ResultStatus and entity field
/// </summary>
/// <typeparam name="T"></typeparam>
public class Result<T>
{
    /// <summary>
    /// instance of the ResultStatus
    /// </summary>
    public ResultStatus Status { get; set; }
    /// <summary>
    /// entity data
    /// </summary>
    public T? Data { get; set; }
    /// <summary>
    /// Result<T/> constructor
    /// </summary>
    public Result()
    {
        Status = new ResultStatus();
    }
}