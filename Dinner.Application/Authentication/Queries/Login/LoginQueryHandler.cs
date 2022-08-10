namespace Dinner.Application.Authentication.Queries.Login;

using Common.Authentication;
using Dinner.Application.Common.Interfaces.Authentication;
using Common.Interfaces.Persistence;
using Domain.Folder.Errors;
using ErrorOr;
using MediatR;

public class LoginQueryHandler : IRequestHandler<LoginQuery, ErrorOr<AuthenticationResult>>
{
    private readonly IJwtTokenGenerator jwtTokenGenerator;
    private readonly IUserRepository userRepository;

    public LoginQueryHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        this.jwtTokenGenerator = jwtTokenGenerator;
        this.userRepository = userRepository;
    }
    
    public async Task<ErrorOr<AuthenticationResult>> Handle(LoginQuery command, CancellationToken cancellationToken)
    {
        var user = this.userRepository.GetUserByEmail(command.Email);
        if (user is null)
        {
            return Errors.Authentication.InvalidCredentials;
        }

        if (user.Password != command.Password)
        {
            return Errors.Authentication.InvalidCredentials;
        }

        var token = this.jwtTokenGenerator.GenerateToken(user);

        return new AuthenticationResult(
            user,
            token);
    }
}