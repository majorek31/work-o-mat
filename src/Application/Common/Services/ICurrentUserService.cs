using WorkOMat.Application.Common.Result;
using WorkOMat.Domain.User;

namespace WorkOMat.Application.Common.Services;

public interface ICurrentUserService
{
    Task<Result<User>> GetCurrentUserAsync(CancellationToken cancellationToken);
}