using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using WorkOMat.Application.Behaviors;
using WorkOMat.Application.Common.Result;

namespace WorkOMat.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddTransient(typeof(IResultFactory<>), typeof(ResultFactory<>));

        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
        services.AddMediator(config =>
        {
            config.ServiceLifetime = ServiceLifetime.Scoped;
            config.PipelineBehaviors = [typeof(ValidationBehavior<,>)];
        });
        return services;
    }
}