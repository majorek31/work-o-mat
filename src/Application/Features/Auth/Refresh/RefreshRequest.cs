using Mediator;
using WorkOMat.Application.Common.Result;

namespace WorkOMat.Application.Features.Auth.Refresh;

public record RefreshRequest(string RefreshToken) : IRequest<Result<RefreshResponse>>;