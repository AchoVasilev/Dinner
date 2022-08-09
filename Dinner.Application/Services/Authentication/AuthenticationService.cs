namespace Dinner.Application.Services.Authentication;

using Common.Interfaces.Authentication;
using Common.Interfaces.Persistence;
using Domain.Entities;

public class AuthenticationService : IAuthenticationService
{
    private readonly IJwtTokenGenerator jwtTokenGenerator;
    private readonly IUserRepository userRepository;

    public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        this.jwtTokenGenerator = jwtTokenGenerator;
        this.userRepository = userRepository;
    }

    public AuthenticationResult Login(string email, string password)
    {
        var user = this.userRepository.GetUserByEmail(email);
        if (user is null)
        {
            throw new Exception("User with this email doesn't exist.");
        }

        if (user.Password != password)
        {
            throw new Exception("Invalid password");
        }

        var token = this.jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(
            user,
            token);
    }

    public AuthenticationResult Register(string firstName, string lastName, string email, string password)
    {
        if (this.userRepository.GetUserByEmail(email) != null)
        {
            throw new Exception("User with this email already exists");
        }

        var user = new User()
        {
            Email = email,
            FirstName = firstName,
            LastName = lastName,
            Password = password
        };

        this.userRepository.Add(user);

        var token = this.jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(
            user,
            token);
    }
}