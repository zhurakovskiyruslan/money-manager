using Microsoft.AspNetCore.Http;
using MoneyManager.Application.Common.Exceptions;

namespace MoneyManager.API.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    public async  Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ApplicationValidationException ex)
        {
            await WriteProblem(context, StatusCodes.Status400BadRequest, "Validation error", errors: ex.Errors);
        }
        catch (NotFoundException ex)
        {
            await WriteProblem(context, StatusCodes.Status404NotFound, ex.Message);
        }
        catch (ForbiddenException ex)
        {
            await WriteProblem(context, StatusCodes.Status403Forbidden, ex.Message);
        }
        catch (ConflictException ex)
        {
            await WriteProblem(context, StatusCodes.Status409Conflict, ex.Message);
        }
        catch (Exception ex)
        {
            await WriteProblem(context, StatusCodes.Status500InternalServerError, "Unexpected error");
        }
        
    }
    private static async Task WriteProblem(
        HttpContext context,
        int status,
        string title,
        string? detail = null,
        object? errors = null)
    {
        context.Response.StatusCode = status;
        context.Response.ContentType = "application/problem+json";

        var problem = new
        {
            type = $"https://httpstatuses.com/{status}",
            title,
            status,
            detail,
            traceId = context.TraceIdentifier,
            errors 
        };

        await context.Response.WriteAsJsonAsync(problem);
    }
}