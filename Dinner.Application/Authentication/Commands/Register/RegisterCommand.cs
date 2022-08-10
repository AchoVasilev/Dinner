namespace Dinner.Application.Authentication.Commands.Register;

using Common.Authentication;
using MediatR;
using ErrorOr;

public record RegisterCommand
    (string FirstName, string LastName, string Email, string Password) 
    : IRequest<ErrorOr<AuthenticationResult>>;