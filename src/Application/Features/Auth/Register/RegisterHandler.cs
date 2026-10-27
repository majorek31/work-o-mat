using WorkOMat.Application.Common.Interfaces;
using WorkOMat.Application.Common.Result;
using Mediator;
namespace WorkOMat.Application.Features.Auth.Register;  

public class RegisterHandler : IRequestHandler<RegisterCommand, Result<Unit>>
{
    private readonly IIdentityService _identityService;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterHandler(IIdentityService identityService, IUnitOfWork unitOfWork)
    {
        _identityService = identityService;
        _unitOfWork = unitOfWork;
    }

    async ValueTask<Result<Unit>> IRequestHandler<RegisterCommand, Result<Unit>>.Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var result = await _identityService.RegisterAsync(request.DisplayName, request.Email, request.Password);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        if (!result.IsSuccess)
        {
            return Result<Unit>.Failure(result.Errors ?? new List<Error> { new Error(ErrorKind.InternalServerError, "Error.Unexpected") });
        }
        return Result<Unit>.Success(Unit.Value);
    }
}