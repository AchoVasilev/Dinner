namespace Dinner.Api.Controllers;

using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

[ApiController]
public class ErrorController : ControllerBase
{
    [Route("/error")]
    public IActionResult Error()
    {
        var exception = this.HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
        
        return this.Problem(title:exception?.Message, statusCode: 500);
    }
}