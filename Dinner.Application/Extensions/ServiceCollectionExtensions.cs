namespace Dinner.Application.Extensions;

using Microsoft.Extensions.DependencyInjection;
using Services.Authentication;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
        => services.AddTransient<IAuthenticationService, AuthenticationService>();
}