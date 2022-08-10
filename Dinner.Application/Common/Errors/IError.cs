namespace Dinner.Application.Common.Errors;

using System.Net;

public interface IError
{
    public HttpStatusCode StatusCode { get; }
    
    public string ErrorMessage { get; }
}