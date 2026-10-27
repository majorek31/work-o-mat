using Mediator;
using WorkOMat.Application.Features.User.Dtos;
using WorkOMat.Application.Features.User.GetMe;
using WorkOMat.WebApi.Extensions;

namespace WorkOMat.WebApi.Endpoints.User;

public class GetMeEndpoint : Endpoint
{
    public override void Map(IEndpointRouteBuilder builder)
    {
        builder.MapGet("/api/user/me", async (IMediator mediator) =>
        {
            var result = await mediator.Send(new GetMeRequest());
            return result.ToHttpResult();
        })
        .WithName("GetMe")
        .RequireAuthorization("Bearer")
        .WithTags("User")
        .WithDescription("Get the current authenticated user's information.")
        .Produces<UserDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status500InternalServerError);
    }
}