using System.Net;

namespace MakeGreyImageAPI.Middlewares;
/// <summary>
/// 
/// </summary>
public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="next"></param>
    public ErrorHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch
        {
            // TODO: add logging 
            var response = context.Response;
            response.StatusCode = StatusCodes.Status500InternalServerError;
        }
    }
}