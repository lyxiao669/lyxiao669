using Juzhen.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Juzhen.MiniProgramAPI
{
    public class ApiExceptionHandlerMiddleware
    {
        readonly RequestDelegate _next;

        readonly ILogger<ApiExceptionHandlerMiddleware> _logger;

        public ApiExceptionHandlerMiddleware(RequestDelegate request, ILogger<ApiExceptionHandlerMiddleware> logger)
        {
            _next = request;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {                
                if (exception is DomainException domainException)
                {
                    await WriteAsJsonAsync(context, new
                    {
                        ret_code = 500,
                        ret_msg = domainException.Message
                    }, 500);
                }
                else if (exception is ServiceException serviceException)
                {
                    await WriteAsJsonAsync(context, new
                    {
                        ret_code = serviceException.StatusCode,
                        ret_msg = serviceException.Message
                    }, serviceException.StatusCode);
                }
                else
                {
                    await WriteAsJsonAsync(context, new
                    {
                        ret_code = 500,
                        ret_msg = "发生了一个预料之外的错误，记录该操作并联系管理员"
                    }, 500);
                }
                _logger.LogError(exception, exception.Message);
            }
        }

        private static Task WriteAsJsonAsync(HttpContext context, object data, int status)
        {
            if (!context.Response.HasStarted)
            {
                context.Response.StatusCode = status;
                return context.Response.WriteAsJsonAsync(data);
            }
            return Task.CompletedTask;
        }
    }
}
