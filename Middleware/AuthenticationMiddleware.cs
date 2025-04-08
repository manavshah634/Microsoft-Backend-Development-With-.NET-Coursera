using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

public class UserAuthMiddleware
{
    private readonly RequestDelegate _requestHandler;
    
    public UserAuthMiddleware(RequestDelegate requestHandler) => _requestHandler = requestHandler;
    
    public async Task Invoke(HttpContext httpContext)
    {
        if (!httpContext.Request.Headers.ContainsKey("Authorization"))
        {
            httpContext.Response.StatusCode = 401;
            await httpContext.Response.WriteAsync("Unauthorized");
            return;
        }
        await _requestHandler(httpContext);
    }
}
