namespace Dinner.Application.Common.Interfaces.Authentication;

using Domain.Entities;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}