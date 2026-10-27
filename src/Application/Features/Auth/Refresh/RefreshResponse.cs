namespace WorkOMat.Application.Features.Auth.Refresh;

public record RefreshResponse(string AccessToken, string RefreshToken, DateTime RefreshTokenExpiry);