namespace Dinner.Application.Common.Errors;

using System.Net;

public interface IServiceException
{
    public HttpStatusCode StatusCode { get; }
    
    public string ErrorMessage { get; }
}