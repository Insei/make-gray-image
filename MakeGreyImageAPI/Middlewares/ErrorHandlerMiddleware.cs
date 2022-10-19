namespace MakeGreyImageAPI.Middlewares;
/// <summary>
/// Middleware for global error handling
/// </summary>
public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;
    /// <summary>
    /// constructor of ErrorHandlerMiddleware
    /// </summary>
    /// <param name="next">RequestDelegate</param>
    public ErrorHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    /// <summary>
    /// Middleware call point
    /// </summary>
    /// <param name="context">HttpContext</param>
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