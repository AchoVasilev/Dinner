namespace Dinner.Api.Controllers;

using Application.Common.Errors;
using Application.Services.Authentication;
using Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;

[Route("auth")]
public class AuthenticationController : ApiController
{
    private readonly IAuthenticationService authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
        => this.authenticationService = authenticationService;

    [HttpPost("register")]
    public IActionResult Register(RegisterRequest request)
    {
        var result = this.authenticationService.Register(
            request.FirstName,
            request.LastName,
            request.Email,
            request.Password);

        return result.Match(
            result => this.Ok(MapAuthResult(result)),
            errors => this.Problem(errors));
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        var result = this.authenticationService.Login(request.Email, request.Password);

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