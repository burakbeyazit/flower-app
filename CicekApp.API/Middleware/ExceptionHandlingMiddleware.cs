using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using CicekApp.Application.Models.Response;

namespace CicekApp.API.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                await HandleException(context, e);
            }
        }

        private static async Task HandleException(HttpContext httpContext, Exception exception)
        {

            var response = new ExceptionResponseModel()
            {
                Message = exception.Message,
                Error = 404
            };

            httpContext.Response.ContentType = "application/json";

            httpContext.Response.StatusCode = 404;

            await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
        }


    }
}