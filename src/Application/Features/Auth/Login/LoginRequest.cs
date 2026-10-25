using Mediator;
using WorkOMat.Application.Common.Result;

namespace WorkOMat.Application.Features.Auth.Login;

public record LoginRequest(string Email, string Password) : IRequest<Result<LoginResponse>>;