namespace Dinner.Application.Authentication.Queries.Login;

using Common.Authentication;
using ErrorOr;
using MediatR;

public record LoginQuery(string Email, string Password) : IRequest<ErrorOr<AuthenticationResult>>;