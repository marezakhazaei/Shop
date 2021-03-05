using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Web
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionMiddleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task Invoke(HttpContext context, ILogger<CustomExceptionMiddleware> logger)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                if (context.Response.HasStarted)
                    throw;

                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";
                context.Response.Headers.Add("exception", "Internal Server Error");
                //var json = System.Text.Json.JsonSerializer.Serialize(new ErrorResultDto { Errors = new List<ErrorDto> { new ErrorDto { Code = Common.Enums.ErrorCode.SystemError, Message = "Internal Server Error" } } }, new System.Text.Json.JsonSerializerOptions { IgnoreNullValues = true });
                logger.Log(LogLevel.Error, ex, "Error");

                await context.Response.WriteAsync("SystemError");
            }
        }

    }
}
