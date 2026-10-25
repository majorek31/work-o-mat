namespace WorkOMat.Application.Common.Result;

public sealed class ResultFactory<T> : IResultFactory<T>
    where T : IResult
{
    T IResultFactory<T>.Failure(List<Error> errors)
    {
        var resultType = typeof(T);
        var valueType = resultType.GetGenericArguments().FirstOrDefault()!;
        var method = typeof(Result<>)
            .MakeGenericType(valueType)
            .GetMethod(nameof(Result<T>.Failure))!;
        return (T)method.Invoke(null, [errors])!;
    }

}