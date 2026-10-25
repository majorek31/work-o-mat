namespace WorkOMat.Application.Common.Result;

public interface IResultFactory<TResponse>
    where TResponse : IResult
{
    TResponse Failure(List<Error> errors);
}