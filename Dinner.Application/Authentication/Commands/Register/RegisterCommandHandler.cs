namespace Dinner.Application.Authentication.Commands.Register;

using Common.Authentication;
using Common.Interfaces.Authentication;
using Common.Interfaces.Persistence;
using Domain.Entities;
using Domain.Folder.Errors;
using ErrorOr;
using MediatR;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator jwtTokenGenerator;
    private readonly IUserRepository userRepository;

    public RegisterCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        this.jwtTokenGenerator = jwtTokenGenerator;
        this.userRepository = userRepository;
    }
    
    public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand comand, CancellationToken cancellationToken)
    {
        if (this.userRepository.GetUserByEmail(comand.Email) != null)
        {
            return Errors.User.DuplicateEmail;
        }

        var user = new User()
        {
            Email = comand.Email,
            FirstName = comand.FirstName,
            LastName = comand.LastName,
            Password = comand.Password
        };

        this.userRepository.Add(user);

        var token = this.jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(
            user,
            token);
    }
}