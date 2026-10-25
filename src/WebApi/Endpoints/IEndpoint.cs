namespace WorkOMat.WebApi.Endpoints;

public interface IEndpoint
{
    void Map(IEndpointRouteBuilder builder);
}