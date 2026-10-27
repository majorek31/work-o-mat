namespace WorkOMat.Application.Common.Result;

public class Result<T> : IResult
{
    public bool IsSuccess { get; init; }
    public T? Value { get; init; }
    public List<Error>? Errors { get; init; }

    private Result(bool isSuccess, T? value, List<Error>? error)
    {
        IsSuccess = isSuccess;
        Value = value;
        Errors = error;
    }

    public static Result<T> Success(T value) => new Result<T>(true, value, null);

    public static Result<T> Failure(List<Error> errors) => new Result<T>(false, default, errors);
    public static Result<T> Failure(Error error) => new Result<T>(false, default, new List<Error> { error });
}