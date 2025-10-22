using DISERTATIE.FLACARA.CAPTURII.DATAACCESS.Connection;
using DISERTATIE.FLACARA.CAPTURII.DATAACCESS.Factory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace DISERTATIE.FLACARA.CAPTURII.CONFIGURATION;
///
public static class RepositoryConfiguration
{
    public static IServiceCollection AddRepositoryConfiguration(this IServiceCollection services, IConfiguration config)
    {
        services.AddSingleton<IDataAccess>(new DataAccess(config.GetConnectionString("DB") ?? string.Empty));

        services.AddSingleton<IDataFactory, DataFactory>();

        return services;
    }
}
