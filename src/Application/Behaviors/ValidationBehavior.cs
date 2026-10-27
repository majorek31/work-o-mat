using FluentValidation;
using Mediator;
using WorkOMat.Application.Common.Result;

namespace WorkOMat.Application.Behaviors;

public sealed class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : IResult
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;
    private readonly IResultFactory<TResponse> _resultFactory;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators, IResultFactory<TResponse> resultFactory)
    {
        _validators = validators;
        _resultFactory = resultFactory;
    }

    public async ValueTask<TResponse> Handle(TRequest message, MessageHandlerDelegate<TRequest, TResponse> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any())
        {
            return await next(message, cancellationToken);
        }

        var ctx = new ValidationContext<TRequest>(message);
        var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(ctx, cancellationToken)));
        var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

        if (failures.Count != 0)
        {
            var errors = failures.Select(f => new Error(ErrorKind.Validation, f.ErrorCode, f.ErrorMessage)).ToList();
            var result = _resultFactory.Failure(errors);
            return result;
        }

        return await next(message, cancellationToken);
    }
}