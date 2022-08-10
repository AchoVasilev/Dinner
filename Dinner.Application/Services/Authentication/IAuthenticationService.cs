namespace Dinner.Application.Services.Authentication;

using FluentResults;
using ErrorOr;

public interface IAuthenticationService
{
    ErrorOr<AuthenticationResult> Login(string email, string password);

   ErrorOr<AuthenticationResult> Register(string firstName, string lastName, string email, string password);
}