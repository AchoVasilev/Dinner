namespace Dinner.Infrastructure.Authentication;

public record LoginRequest(
    string Email,
    string Password);