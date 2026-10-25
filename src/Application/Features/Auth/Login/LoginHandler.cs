using Mediator;
using WorkOMat.Application.Common.Interfaces;
using WorkOMat.Application.Common.Repositories;
using WorkOMat.Application.Common.Result;
using WorkOMat.Domain.RefreshToken;
using WorkOMat.Domain.ValueObjects;

namespace WorkOMat.Application.Features.Auth.Login;

public class LoginHandler(
    ITokenService tokenService,
    IIdentityService identityService,
    IRefreshTokenRepository refreshTokenRepository,
    IUnitOfWork unitOfWork
) : IRequestHandler<LoginRequest, Result<LoginResponse>>
{
    public async ValueTask<Result<LoginResponse>> Handle(LoginRequest request, CancellationToken cancellationToken)
    {
        var result = await identityService.LoginAsync(request.Email, request.Password);
        if (!result.IsSuccess)
        {
            return Result<LoginResponse>.Failure(result.Errors ?? new List<Error> { new Error(ErrorKind.InternalServerError, "Error.Unexpected") });
        }
        var user = result.Value!;
        if (result.Value is null)
        {
            return Result<LoginResponse>.Failure(new List<Error> { new Error(ErrorKind.InternalServerError, "Error.Unexpected") });
        }
        var userId = user.Id;
        var userEmail = new Email(request.Email);

        var accessToken = tokenService.GenerateAccessToken(userId, userEmail);

        var refreshTokenValue = tokenService.GenerateRefreshToken(userId, userEmail);

        var refreshToken = new RefreshToken(
            RefreshTokenId.New(),
            refreshTokenValue.Value,
            refreshTokenValue.Expiry,
            userId
        );
        await refreshTokenRepository.AddAsync(refreshToken, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Result<LoginResponse>.Success(new LoginResponse(accessToken.ToString(), refreshTokenValue.ToString(), refreshTokenValue.Expiry));
    }
}