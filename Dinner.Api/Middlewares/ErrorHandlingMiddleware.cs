namespace Dinner.Api.Middlewares;

using System.Net;
using System.Text.Json;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate next;

    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            await this.next(httpContext);
        }
        catch (Exception e)
        {
            await this.HandleException(httpContext, e);
        }
    }

    private async Task HandleException(HttpContext httpContext, Exception exception)
    {
        var code = HttpStatusCode.InternalServerError;
        var result = JsonSerializer.Serialize(new { error = "An error occured white processing your request." });
        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = (int)code;

        await httpContext.Response.WriteAsync(result);
    }
}