namespace MakeGreyImageAPI.DTOs;

public class Status
{
    /// <summary>
    /// 
    /// </summary>
    public int Code { get; set; } = 0;
    /// <summary>
    /// 
    /// </summary>
    public string Message { get; set; } = "";
}
public class EmptyApiResponse
{
    /// <summary>
    /// 
    /// </summary>
    public Status Status { get; set; } = new();
}
public class ApiResponse<T> : EmptyApiResponse
{
    /// <summary>
    /// 
    /// </summary>
    public T? Data { get; set; }
}
