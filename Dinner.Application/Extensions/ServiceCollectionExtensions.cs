namespace Dinner.Application.Extensions;

using MediatR;
using Microsoft.Extensions.DependencyInjection;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
        => services.AddMediatR(typeof(ServiceCollectionExtensions).Assembly);
}