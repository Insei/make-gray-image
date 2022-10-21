using System.Net;
using System.Text;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MakeGreyImageAPI.Middlewares;
/// <summary>
/// Middleware for global error handling
/// </summary>
public class ErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private Interfaces.ILogger _logger;

    /// <summary>
    /// Constructor of ErrorHandlerMiddleware
    /// </summary>
    /// <param name="next">RequestDelegate</param>
    /// <param name="logger">Logger implementation</param>
    public ErrorHandlerMiddleware(RequestDelegate next, Interfaces.ILogger logger)
    {
        _next = next;
        _logger = logger;
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
        catch (Exception ex)
        {
            
            _logger.LogError(ex, "An unhandled exception has occurred while executing the request.\n" +
                                 "\tUrl: "+ $"{context.Request.GetDisplayUrl()}.\n " +
                                 "\tRequest Data: " + GetRequestData(context));
            
            var response = context.Response;
            response.StatusCode = StatusCodes.Status500InternalServerError;
        }
    }
    /// <summary>
    /// Get data from request
    /// </summary>
    /// <param name="context">Data context</param>
    /// <returns>Data in string format</returns>
    private static string GetRequestData(HttpContext context)
    {
        var sb = new StringBuilder();

        if (context.Request.Query.Any())
        {
            sb.Append("\n\tQuery variables:\n");
            foreach (var (key, value) in context.Request.Query)
            {
                sb.Append($"\t\tKey={key}, Value={value}\n");
            }
        }

        sb.AppendLine("\tMethod: " + context.Request.Method);

        return sb.ToString();
    }
}