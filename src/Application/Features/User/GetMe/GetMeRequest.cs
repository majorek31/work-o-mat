using Mediator;
using WorkOMat.Application.Common.Result;
using WorkOMat.Application.Features.User.Dtos;

namespace WorkOMat.Application.Features.User.GetMe;

public record GetMeRequest : IRequest<Result<UserDto>>;