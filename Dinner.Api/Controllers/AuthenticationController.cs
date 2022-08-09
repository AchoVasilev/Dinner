namespace Dinner.Api.Controllers;

using Application.Services.Authentication;
using Contracts.Authentication;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("auth")]
public class AuthenticationController : ControllerBase
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

        var response = new AuthenticationResponse(
            result.Id, 
            result.FirstName, 
            result.LastName, 
            result.Email, 
            result.Token);
        
        return this.Ok(response);
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequest request)
    {
        var result = this.authenticationService.Login(request.Email, request.Password);
        
        var response = new AuthenticationResponse(
            result.Id, 
            result.FirstName, 
            result.LastName, 
            result.Email, 
            result.Token);
        
        return this.Ok(response);
    }
}