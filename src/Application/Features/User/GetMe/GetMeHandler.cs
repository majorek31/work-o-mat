using Mediator;
using WorkOMat.Application.Common.Result;
using WorkOMat.Application.Features.User.Dtos;

namespace WorkOMat.Application.Features.User.GetMe;

public class GetMeHandler : IRequestHandler<GetMeRequest, Result<UserDto>>
{

    public GetMeHandler()
    {
    }

    async ValueTask<Result<UserDto>> IRequestHandler<GetMeRequest, Result<UserDto>>.Handle(GetMeRequest request, CancellationToken cancellationToken)
    {
        return Result<UserDto>.Failure(new Error(ErrorKind.InternalServerError, "Error.NotImplemented"));
    }
}