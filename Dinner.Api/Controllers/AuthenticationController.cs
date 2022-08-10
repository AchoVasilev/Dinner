namespace Dinner.Api.Controllers;

using Application.Authentication.Commands.Register;
using Application.Authentication.Queries.Login;
using Application.Common.Authentication;
using Contracts.Authentication;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[Route("auth")]
public class AuthenticationController : ApiController
{
    private readonly IMediator mediator;
    private readonly IMapper mapper;

    public AuthenticationController(IMediator mediator, IMapper mapper)
    {
        this.mediator = mediator;
        this.mapper = mapper;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var command = this.mapper.Map<RegisterCommand>(request);

        var authResult = await this.mediator.Send(command);

        return authResult.Match(
            result => this.Ok(this.mapper.Map<AuthenticationResponse>(result)),
            errors => this.Problem(errors));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var query = this.mapper.Map<LoginQuery>(request);

        var result = await this.mediator.Send(query);

        return result.Match(
            result => this.Ok(this.mapper.Map<AuthenticationResponse>(result)),
            errors => this.Problem(errors));
    }
}