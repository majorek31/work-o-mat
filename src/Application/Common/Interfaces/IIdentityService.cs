
using WorkOMat.Application.Common.Result;
using WorkOMat.Domain.User;

namespace WorkOMat.Application.Common.Interfaces;

public interface IIdentityService
{
    Task<Result<User>> RegisterAsync(string displayName, string email, string password);
    Task<Result<User>> LoginAsync(string email, string password);
}