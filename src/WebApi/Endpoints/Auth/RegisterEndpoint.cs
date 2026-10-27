using Mediator;
using WorkOMat.Application.Common.Result;
using WorkOMat.Application.Features.Auth.Register;
using WorkOMat.WebApi.Extensions;

namespace WorkOMat.WebApi.Endpoints.Auth;

public sealed class RegisterEndpoint : Endpoint
{
    public override void Map(IEndpointRouteBuilder builder)
    {
        Configure<RegisterCommand, Result<Unit>>(
            builder.MapPost("/api/auth/register", 
            async (RegisterCommand command, IMediator mediator) =>
        {
            var result = await mediator.Send(command);
            return result.IsSuccess ? Results.Created() : result.ToHttpResult();
        }),
        "Register",
        new[] { "Auth" },
        "Registers a new user."
        )
        .Produces<Unit>(StatusCodes.Status201Created)
        .Produces<Unit>(StatusCodes.Status409Conflict);
    }
}