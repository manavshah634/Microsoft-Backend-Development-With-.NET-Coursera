using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _nextHandler;
    
    public RequestLoggingMiddleware(RequestDelegate nextHandler) => _nextHandler = nextHandler;
    
    public async Task Invoke(HttpContext httpContext)
    {
        Console.WriteLine($"Request: {httpContext.Request.Method} {httpContext.Request.Path}");
        await _nextHandler(httpContext);
        Console.WriteLine($"Response: {httpContext.Response.StatusCode}");
    }
}