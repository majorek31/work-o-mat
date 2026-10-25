using Mediator;
using WorkOMat.Application.Common.Result;
namespace WorkOMat.Application.Features.Auth.Register;

public record RegisterCommand(string DisplayName, string Email, string Password) : IRequest<Result<Unit>>;