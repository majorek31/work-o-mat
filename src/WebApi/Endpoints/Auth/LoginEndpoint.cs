using Mediator;
using Microsoft.AspNetCore.Identity.Data;
using WorkOMat.Application.Features.Auth.Login;
using WorkOMat.WebApi.Extensions;

namespace WorkOMat.WebApi.Endpoints.Auth;

public class LoginEndpoint : Endpoint
{
    public override void Map(IEndpointRouteBuilder builder)
    {
        Configure<Application.Features.Auth.Login.LoginRequest, LoginResponse>(
            builder.MapPost("/api/auth/login", 
            async (Application.Features.Auth.Login.LoginRequest request, IMediator mediator) =>
        {
            var result = await mediator.Send(request);
            return result.IsSuccess ? Results.Ok(result.Value) : result.ToIResult();
        }),
        "Login",
        new[] { "Auth" },
        "Logs in a user and returns an access token."
        )
        .Produces<LoginResponse>(StatusCodes.Status200OK)
        .Produces<Unit>(StatusCodes.Status400BadRequest)
        .Produces<Unit>(StatusCodes.Status404NotFound);
    }
}