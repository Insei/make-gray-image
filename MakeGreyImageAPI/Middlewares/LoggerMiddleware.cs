using System.Text;

namespace MakeGreyImageAPI.Middlewares;
/// <summary>
/// Middleware for logging http requests
/// </summary>
public class LoggerMiddleware
{
    private readonly RequestDelegate _next;
    private Interfaces.ILogger _logger;
    
    /// <summary>
    /// LoggerMiddleware constructor
    /// </summary>
    /// <param name="next">RequestDelegate</param>
    /// <param name="logger">Logger implementation</param>
    public LoggerMiddleware(RequestDelegate next, Interfaces.ILogger logger)
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
            var requestBody = await ReadRequestBody(context.Request);
            _logger.Log($"HTTP request information:\n" +
                        $"\tMethod: {context.Request.Method}\n" +
                        $"\tPath: {context.Request.Path}\n" +
                        $"\tQueryString: {context.Request.QueryString}\n" +
                        $"\tHeaders: {FormatHeaders(context.Request.Headers)}\n" +
                        $"\tSchema: {context.Request.Scheme}\n" +
                        $"\tHost: {context.Request.Host}\n" +
                        $"\tBody: {requestBody}");
        }
        catch 
        {
          //Ignore
        }
        var originalResponseBody = context.Response.Body;
        await using var newResponseBody = new MemoryStream();
        context.Response.Body = newResponseBody;
        
        await _next(context);
        
        try
        {
            newResponseBody.Seek(0, SeekOrigin.Begin);
            var responseBody = await ReadResponseBody(context.Response);
            _logger.Log($"HTTP response information:\n" +
                        $"\tStatusCode: {context.Response.StatusCode}\n" +
                        $"\tContentType: {context.Response.ContentType}\n" +
                        $"\tHeaders: {FormatHeaders(context.Response.Headers)}\n" +
                        $"\tBody: {responseBody}");
        }
        catch 
        {
            //Ignore
        }
        
        newResponseBody.Seek(0, SeekOrigin.Begin);
        await newResponseBody.CopyToAsync(originalResponseBody);
    }
    
    private static string FormatHeaders(IHeaderDictionary headers) => 
        string.Join(", ", headers.Select(kvp => $"{{{kvp.Key}: {string.Join(", ", kvp.Value)}}}"));
    
    private async Task<string> ReadRequestBody(HttpRequest request)
    {
        HttpRequestRewindExtensions.EnableBuffering(request);

        var body = request.Body;
        var buffer = new byte[Convert.ToInt32(request.ContentLength)];
        await request.Body.ReadAsync(buffer, 0, buffer.Length);
        var requestBody = Encoding.UTF8.GetString(buffer);
        body.Seek(0, SeekOrigin.Begin);
        request.Body = body;

        return $"{requestBody}";
    }

    private static async Task<string> ReadResponseBody(HttpResponse response)
    {
        response.Body.Seek(0, SeekOrigin.Begin);
        var responseBody = await new StreamReader(response.Body).ReadToEndAsync();
        response.Body.Seek(0, SeekOrigin.Begin);

        return $"{responseBody}";
    }
}