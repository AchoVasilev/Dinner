namespace Dinner.Infrastructure.Extensions;

using Application.Common.Interfaces.Authentication;
using Application.Common.Services;
using Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
        => services.Configure<JwtSettings>(configuration.GetSection(nameof(JwtSettings)))
            .AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>()
            .AddSingleton<IDateTimeProvider, DateTimeProvider>();
}