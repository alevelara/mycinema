using Mycinema.Application.Exceptions;
using Microsoft.AspNetCore.Http;
using System.Net;
using Newtonsoft.Json;
using Mycinema.API.Errors;

namespace Mycinema.API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _environment;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment environment)
        {
            _next = next;
            _logger = logger;
            _environment = environment;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                context.Response.ContentType = "application/json";
                var statusCode = (int)HttpStatusCode.InternalServerError;
                var resultMessage = string.Empty;

                switch (e)
                {
                    case NotFoundException notFoundException:
                        statusCode = (int)HttpStatusCode.NotFound;
                        break;
                    case ValidationException validationException:
                        statusCode = (int)HttpStatusCode.BadRequest;
                        var validationJson = JsonConvert.SerializeObject(validationException.Errors);
                        resultMessage = JsonConvert.SerializeObject(new CodeErrorException(statusCode, e.Message, validationJson));
                        break;
                    case BadRequestException badRequestException:
                        statusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    default:
                        break;
                }

                context.Response.StatusCode = statusCode;

                if (string.IsNullOrEmpty(resultMessage))
                    resultMessage = JsonConvert.SerializeObject(new CodeErrorException(statusCode, e.Message, e.StackTrace));

                await context.Response.WriteAsync(resultMessage);
            }
        }
    }
}
