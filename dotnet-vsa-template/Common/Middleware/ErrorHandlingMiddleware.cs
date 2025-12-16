using Serilog;
using System.Net;

namespace dotnet_vsa_template.Common.Middleware
{
  public class ErrorHandlingMiddleware
  {
    private readonly RequestDelegate _next;

    public ErrorHandlingMiddleware(RequestDelegate next)
    {
      _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
      try
      {
        await _next(context);
      }
      catch (Exception ex)
      {
        Log.Error(ex, "An unhandled exception occurred while processing the request.");
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Response.ContentType = "application/json";
        var result = System.Text.Json.JsonSerializer.Serialize(new { error = "An unexpected error occurred." });
        await context.Response.WriteAsync(result);
      }
    }
  }
}