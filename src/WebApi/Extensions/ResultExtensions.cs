using WorkOMat.Application.Common.Result;

namespace WorkOMat.WebApi.Extensions;

public static class ResultExtensions
{
    public static Microsoft.AspNetCore.Http.IResult ToIResult<T>(this Result<T> result)
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
        
        var groupedErrors = errors.GroupBy(e => e.Kind)
            .ToDictionary(g => g.Key.ToString(), g => g.Select(e => e.Code).ToList());
        
        return Results.Problem(
            statusCode: statusCode,
            extensions: new Dictionary<string, object?>
            {
                ["Errors"] = groupedErrors
            }
        );

    }
}