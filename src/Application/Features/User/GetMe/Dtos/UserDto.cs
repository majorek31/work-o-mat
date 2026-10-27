namespace WorkOMat.Application.Features.User.Dtos;

public sealed record UserDto(
    Guid Id,
    string DisplayName,
    string Email
);