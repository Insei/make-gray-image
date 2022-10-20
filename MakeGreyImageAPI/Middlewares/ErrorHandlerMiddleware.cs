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
            
            _logger.LogError(ex, $"An unhandled exception has occurred while executing the request. Url: " +
                                 $"{context.Request.GetDisplayUrl()}. Request Data: " + GetRequestData(context));
            
            // // TODO: add logging 
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

        if (context.Request.HasFormContentType && context.Request.Form.Any())
        {
            sb.Append("Form variables:");
            foreach (var x in context.Request.Form)
            {
                sb.AppendFormat("Key={0}, Value={1}<br/>", x.Key, x.Value);
            }
        }

        sb.AppendLine("Method: " + context.Request.Method);

        return sb.ToString();
    }
}