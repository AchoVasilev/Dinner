namespace Dinner.Application.Services.Authentication;

using Domain.Entities;

public record AuthenticationResult(
    User User,
    string Token);