namespace WorkOMat.Application.Common.Result;

public interface IResult
{
    bool IsSuccess { get; init; }
    bool IsFailure => !IsSuccess;
    List<Error>? Errors { get; init; }
}