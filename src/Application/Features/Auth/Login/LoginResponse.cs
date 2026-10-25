namespace WorkOMat.Application.Features.Auth.Login;

public record LoginResponse(string AccessToken, string RefreshToken, DateTime RefreshTokenExpiry);