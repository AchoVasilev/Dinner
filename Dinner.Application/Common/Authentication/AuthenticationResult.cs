namespace Dinner.Application.Common.Authentication;

using Domain.Entities;

public record AuthenticationResult(
    User User,
    string Token);