using System;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _nextMiddleware;
    
    public ExceptionHandlingMiddleware(RequestDelegate nextMiddleware) => _nextMiddleware = nextMiddleware;
    
    public async Task Invoke(HttpContext httpContext)
    {
        try { await _nextMiddleware(httpContext); }
        catch (Exception exception)
        {
            httpContext.Response.StatusCode = 500;
            httpContext.Response.ContentType = "application/json";
            await httpContext.Response.WriteAsync(JsonSerializer.Serialize(new { error = "Internal server error." }));
        }
    }
}