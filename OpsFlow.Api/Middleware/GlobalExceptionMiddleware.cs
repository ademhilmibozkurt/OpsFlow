using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Security.Authentication;
using OpsFlow.Api.Contracts;
using OpsFlow.Application.Common.Exceptions;

namespace OpsFlow.Api.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware
        (
            RequestDelegate next,
            ILogger<GlobalExceptionMiddleware> logger
        )
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(Exception e)
            {
                _logger.LogError(e, "Unhandled exception occured.");
                await HandleExceptionAsync(context, e);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception e)
        {
            ErrorResponse response = new ErrorResponse();
            HttpStatusCode statusCode;

            switch(e)
            {
                case ValidationException validationException:
                    statusCode = HttpStatusCode.BadRequest;
                    response = new ErrorResponse
                    {
                        StatusCode = (int)statusCode,
                        Message = validationException.Message
                    };
                    break;

                case NotFoundException notFoundException:
                    statusCode = HttpStatusCode.NotFound;
                    response = new ErrorResponse
                    {
                        StatusCode = (int)statusCode,
                        Message = notFoundException.Message
                    };
                    break;

                case ForbiddenException forbiddenException:
                    statusCode = HttpStatusCode.Forbidden;
                    response = new ErrorResponse
                    {
                        StatusCode = (int)statusCode,
                        Message = forbiddenException.Message
                    };
                    break;

                case UnauthorizedAccessException:
                case AuthenticationException:
                    statusCode = HttpStatusCode.Unauthorized;
                    response = new ErrorResponse
                    {
                        StatusCode = (int)statusCode,
                        Message = "Unauthorized access."
                    };
                    break;

                default:
                    statusCode = HttpStatusCode.InternalServerError;
                    response = new ErrorResponse
                    {
                        StatusCode = (int)statusCode,
                        Message = "An unexpected error occured."
                    };
                    break;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            await context.Response.WriteAsJsonAsync(response);
        }
    }
}