namespace Dinner.Application.Services.Authentication;

using Common.Interfaces.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IJwtTokenGenerator jwtTokenGenerator;

    public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator) 
        => this.jwtTokenGenerator = jwtTokenGenerator;

    public AuthenticationResult Login(string email, string password)
    {
        return new AuthenticationResult(
            Guid.NewGuid(),
            "firstName",
            "lastName",
            email,
            "token");
    }

    public AuthenticationResult Register(string firstName, string lastName, string email, string password)
    {
        var userId = Guid.NewGuid();
        var token = this.jwtTokenGenerator.GenerateToken(userId, firstName, lastName);
        
        return new AuthenticationResult(
            userId,
            firstName,
            lastName,
            email,
            token);
    }
}