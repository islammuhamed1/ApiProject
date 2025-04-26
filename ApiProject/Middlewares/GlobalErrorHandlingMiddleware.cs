using Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Shared.ErrorModels;
using System.Net;

namespace ApiProject.Middlewares
{
    public class GlobalErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalErrorHandlingMiddleware> _Logger;
        public GlobalErrorHandlingMiddleware(RequestDelegate next, ILogger<GlobalErrorHandlingMiddleware> logger)
        {
            _next = next;
            _Logger = logger;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
                if (httpContext.Response.StatusCode == (int)HttpStatusCode.NotFound)
                {
                    await HandleNotFoundEndPointAsync(httpContext);
                }
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, "An unhandled exception occurred while processing the request.");
                await HandleExceptionAsync(httpContext, ex);
            }
        }
        private async Task HandleNotFoundEndPointAsync(HttpContext httpContext)
        {
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            httpContext.Response.ContentType = "application/json";
            var response = new ErrorDetails
            {
                ErrorMessage = $"The End Point {httpContext.Request.Path} Not Found!",
                SatusCode = (int)HttpStatusCode.NotFound
            }.ToString();

            await httpContext.Response.WriteAsync(response);
        }
        private async Task HandleExceptionAsync(HttpContext htttpContext, Exception ex)
        {
            htttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            htttpContext.Response.ContentType = "application/json";
            var response = new ErrorDetails
            {
                ErrorMessage = ex.Message,
            };
            htttpContext.Response.StatusCode = ex switch
            {
                NotFoundException => (int)HttpStatusCode.NotFound,
                ValidationException validationException => HandleValidationException(validationException,response) ,
                _ => (int)HttpStatusCode.InternalServerError
            };
            response.SatusCode = htttpContext.Response.StatusCode;


            await htttpContext.Response.WriteAsync(response.ToString());
        }
        private int HandleValidationException(ValidationException ex , ErrorDetails errorDetails)
        {
            errorDetails.Errors = ex.Errors;
            return (int)HttpStatusCode.BadRequest;

        }

    }
}
