using EdgyElegance.Api.Models;
using EdgyElegance.Application.Exception;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EdgyElegance.Api.Middlewares;

public class ExceptionHandlerMiddleware : IMiddleware {
    public async Task InvokeAsync(HttpContext context, RequestDelegate next) {
        try {
            await next(context);
        } catch (Exception ex) {
            await HandleException(context, ex);
        }
    }

    private async Task HandleException(HttpContext context, Exception ex) {
        HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
        ProblemDetails? error = null;

        switch(ex) {
            case BadRequestException badRequestException:
                statusCode = HttpStatusCode.BadRequest;
                error = new CustomProblemDetails {
                    Title = badRequestException.Message,
                    Status = (int)statusCode,
                    Detail = badRequestException.InnerException?.Message,
                    Type = nameof(BadRequestException),
                    Errors = badRequestException.ValidationErrors
                };
                break;
            case NotFoundException notFound:
                statusCode = HttpStatusCode.NotFound;
                error = new CustomProblemDetails {
                    Title = notFound.Message,
                    Status = (int)statusCode,
                    Detail = notFound.InnerException?.Message,
                    Type = nameof(BadRequestException),
                    Errors = new Dictionary<string, string[]> { { "Error", new[] { notFound.Message } } }
                };
                break;
            default:
                error = new CustomProblemDetails {
                    Title = "Internal Server Error",
                    Status = (int)statusCode,
                    Detail = null,
                    Type = nameof(HttpStatusCode.InternalServerError),
                    Errors = new Dictionary<string, string[]>()
                };
                break;
        }

        context.Response.StatusCode = (int)statusCode;
        await context.Response.WriteAsJsonAsync(error);
    }
}
