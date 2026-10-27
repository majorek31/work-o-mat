using WorkOMat.Application.Common.Result;

namespace WorkOMat.WebApi.Extensions;

public static class ResultExtensions
{
    public static Microsoft.AspNetCore.Http.IResult ToHttpResult<T>(this Result<T> result)
    {
        if (result.IsSuccess)
        {
            return Results.Ok(result.Value);
        }
        var errors = result.Errors;
        if (errors == null || errors.Count == 0)
        {
            return Results.Problem("An unknown error occurred.", statusCode: StatusCodes.Status500InternalServerError);
        }

        var statusCode = errors.Any(e => e.Kind == ErrorKind.Validation) ? StatusCodes.Status400BadRequest :
                         errors.Any(e => e.Kind == ErrorKind.NotFound) ? StatusCodes.Status404NotFound :
                         errors.Any(e => e.Kind == ErrorKind.Conflict) ? StatusCodes.Status409Conflict :
                         StatusCodes.Status500InternalServerError;
        
        var groupedErrors = GroupErrorsByCode(errors);
        
        return Results.Problem(
            statusCode: statusCode,
            extensions: new Dictionary<string, object?>
            {
                ["Errors"] = groupedErrors
            }
        );

    }

    private static Dictionary<string, List<object?>> GroupErrorsByCode(List<Error> errors)
    {
        var groupedErrors = errors
            .GroupBy(e => e.Code.Split('.').First() ?? "Unknown")
            .ToDictionary(
                g => g.Key,
                g => g.Select(e => (object?)new { Code = e.Code, Message = e.Message }).ToList()
            );

        return groupedErrors;
    }
}