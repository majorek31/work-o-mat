using FluentValidation;
using Mediator;
using WorkOMat.Application.Common.Result;

namespace WorkOMat.Application.Behaviors;

public sealed class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : Result<Unit>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
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
            var errors = failures.Select(f => new Error(ErrorKind.Validation, f.ErrorMessage)).ToList();
            return (TResponse)Result<Unit>.Failure(errors);
        }

        return await next(message, cancellationToken);
    }
}