using Mediator;
using WorkOMat.Application.Common.Interfaces;
using WorkOMat.Application.Common.Repositories;
using WorkOMat.Application.Common.Result;
using WorkOMat.Domain.ValueObjects;

namespace WorkOMat.Application.Features.Auth.Refresh;

public class RefreshHandler : IRequestHandler<RefreshRequest, Result<RefreshResponse>>
{
    private readonly ITokenService _tokenService;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RefreshHandler(ITokenService tokenService, IRefreshTokenRepository refreshTokenRepository, IUnitOfWork unitOfWork)
    {
        _tokenService = tokenService;
        _refreshTokenRepository = refreshTokenRepository;
        _unitOfWork = unitOfWork;
    }

    public async ValueTask<Result<RefreshResponse>> Handle(RefreshRequest request, CancellationToken cancellationToken)
    {
        var tokenResult = await _refreshTokenRepository.GetByTokenAsync(request.RefreshToken, cancellationToken);
        if (tokenResult is null)
        {
            return Result<RefreshResponse>.Failure(new List<Error> { new Error(ErrorKind.Unauthorized, "RefreshToken.Invalid") });
        }
        var newRefreshTokenValue = _tokenService.GenerateRefreshToken(tokenResult.UserId, Email.Create(string.Empty));

        tokenResult.UpdateToken(newRefreshTokenValue.Value, newRefreshTokenValue.Expiry);
        _refreshTokenRepository.Update(tokenResult);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<RefreshResponse>.Success(new RefreshResponse(
            AccessToken: _tokenService.GenerateAccessToken(tokenResult.UserId, Email.Create(string.Empty)).Value,
            RefreshToken: newRefreshTokenValue.Value,
            RefreshTokenExpiry: newRefreshTokenValue.Expiry
        ));
    }
}