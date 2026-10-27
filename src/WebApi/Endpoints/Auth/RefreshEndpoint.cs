using Mediator;
using WorkOMat.Application.Features.Auth.Refresh;
using WorkOMat.WebApi.Extensions;

namespace WorkOMat.WebApi.Endpoints.Auth;

public class RefreshEndpoint : Endpoint
{
    public override void Map(IEndpointRouteBuilder builder)
    {
        Configure<RefreshRequest, RefreshResponse>(
            builder.MapPost("/api/auth/refresh", 
            async (RefreshRequest request, IMediator mediator) =>
        {
            var result = await mediator.Send(new RefreshRequest(request.RefreshToken));
            return result.IsSuccess ? Results.Ok(result.Value) : result.ToHttpResult();
        }),
        "Refresh",
        new[] { "Auth" },
        "Refreshes an access token using a refresh token."
        );
    }
}