using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using WorkOMat.Application.Common.Repositories;
using WorkOMat.Application.Common.Result;
using WorkOMat.Application.Common.Services;
using WorkOMat.Domain.User;

namespace WorkOMat.Infrastructure.Implementations;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IUserRepository _userRepository;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor, IUserRepository userRepository)
    {
        _httpContextAccessor = httpContextAccessor;
        _userRepository = userRepository;
    }

    async Task<Result<User>> ICurrentUserService.GetCurrentUserAsync(CancellationToken cancellationToken)
    {
        var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
        {
            return Result<User>.Failure(new Error(ErrorKind.Unauthorized, "Error.Unauthorized"));
        }
        var user = await _userRepository.GetByIdAsync(userId, cancellationToken);
        
        return user is not null
            ? Result<User>.Success(user)
            : Result<User>.Failure(new Error(ErrorKind.NotFound, "Error.UserNotFound"));
    }
}