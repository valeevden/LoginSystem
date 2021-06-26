using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;

namespace LoginSystem.Middleware
{
    public static class ExceptionMiddleware
    {
        public static async Task ExceptionHandleOptions(HttpContext context, Func<Task> func)
        {
            var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
            if (contextFeature != null)
            {
                var error = contextFeature.Error;
                var message = error.Message;
                var code = HttpStatusCode.InternalServerError;

                switch (error)
                {
                    case ArgumentException _:
                        code = HttpStatusCode.BadRequest;
                        message = "";
                        break;
                    case UnauthorizedAccessException _:
                        code = HttpStatusCode.Unauthorized;
                        message = "Access denied.";
                        break;
                }

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)code;
                await context.Response.WriteAsync(message);
            }
        }

    }
}
