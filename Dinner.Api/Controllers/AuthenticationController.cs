namespace Dinner.Api.Controllers;

using Application.Authentication.Commands.Register;
using Application.Authentication.Queries.Login;
using Application.Common.Authentication;
using Contracts.Authentication;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[Route("auth")]
public class AuthenticationController : ApiController
{
    private readonly IMediator mediator;


    public AuthenticationController(IMediator mediator) 
        => this.mediator = mediator;

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var command = new RegisterCommand(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password);

        var authResult = await this.mediator.Send(command);

        return authResult.Match(
            result => this.Ok(MapAuthResult(result)),
            errors => this.Problem(errors));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var query = new LoginQuery(request.Email, request.Password);

        var result = await this.mediator.Send(query);

        return result.Match(
            result => this.Ok(MapAuthResult(result)),
            errors => this.Problem(errors));
    }

    private static AuthenticationResponse MapAuthResult(AuthenticationResult authResult)
        => new AuthenticationResponse(
            authResult.User.Id,
            authResult.User.FirstName,
            authResult.User.LastName,
            authResult.User.Email,
            authResult.Token);
}