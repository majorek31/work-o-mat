namespace WorkOMat.WebApi.Endpoints;

public abstract class Endpoint : IEndpoint
{
    public abstract void Map(IEndpointRouteBuilder builder);

    protected RouteHandlerBuilder Configure<TResponse>(RouteHandlerBuilder builder,
        string name,
        string[]? tags = null,
        string description = "") where TResponse : class
    {
        return builder
            .WithName(name)
            .WithTags(tags ?? ["Default"])
            .WithDescription(description)
            .Produces(StatusCodes.Status500InternalServerError);
    }

    protected RouteHandlerBuilder Configure<TRequest, TResponse>(RouteHandlerBuilder builder,
        string name,
        string[]? tags = null,
        string description = "") where TResponse : class
    {
        return builder
            .WithName(name)
            .WithTags(tags ?? ["Default"])
            .WithDescription(description)
            .Produces<TResponse>(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status500InternalServerError);
    }
}