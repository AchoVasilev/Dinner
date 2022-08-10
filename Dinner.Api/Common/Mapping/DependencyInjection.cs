namespace Dinner.Api.Common.Mapping;

using System.Reflection;
using Mapster;
using MapsterMapper;

public static class DependencyInjection
{
    public static IServiceCollection AddMappings(this IServiceCollection services)
    {
        var typeConfig = TypeAdapterConfig.GlobalSettings;
        typeConfig.Scan(Assembly.GetExecutingAssembly());

        services.AddSingleton(typeConfig);
        services.AddTransient<IMapper, ServiceMapper>();

        return services;
    }
}