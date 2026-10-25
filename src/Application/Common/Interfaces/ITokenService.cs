using WorkOMat.Domain.User;
using WorkOMat.Domain.ValueObjects;
using WorkOMat.Domain.ValueObjects.Auth;

namespace WorkOMat.Application.Common.Interfaces;

public interface ITokenService
{
    AccessToken GenerateAccessToken(UserId userId, Email email);
    RefreshToken GenerateRefreshToken(UserId userId, Email email);

}