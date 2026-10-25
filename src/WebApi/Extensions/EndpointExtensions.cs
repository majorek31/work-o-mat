using WorkOMat.WebApi.Endpoints;

namespace WorkOMat.WebApi.Extensions;

public static class EndpointExtensions
{
    public static void MapEndpoints(this WebApplication app)
    {
        var endpointTypes = typeof(Program).Assembly.GetTypes()
            .Where(t => typeof(IEndpoint).IsAssignableFrom(t) && !t.IsAbstract);

        foreach (var endpointType in endpointTypes)
        {
            if (Activator.CreateInstance(endpointType) is IEndpoint endpoint)
            {
                endpoint.Map(app);
            }
        }
    }
}