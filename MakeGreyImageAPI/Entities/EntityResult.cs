namespace MakeGreyImageAPI.Entities;
/// <summary>
/// 
/// </summary>
public class ResultStatus
{
    /// <summary>
    /// 
    /// </summary>
    public bool Succeeded { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public List<string> Messages { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public ResultStatus()
    {
        Succeeded = true;
        Messages = new List<string>();
    }
}
/// <summary>
/// 
/// </summary>
public class Result
{
    /// <summary>
    /// 
    /// </summary>
    public ResultStatus Status { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public Result()
    {
        Status = new ResultStatus();
    }
}
/// <summary>
/// 
/// </summary>
/// <typeparam name="T"></typeparam>
public class Result<T>
{
    /// <summary>
    /// 
    /// </summary>
    public ResultStatus Status { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public T? Data { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public Result()
    {
        Status = new ResultStatus();
    }
}