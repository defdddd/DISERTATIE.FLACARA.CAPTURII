using DISERTATIE.FLACARA.CAPTURII.MAPPERS;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DISERTATIE.FLACARA.CAPTURII.DATAACCESS.Data.Contracts;
using DISERTATIE.FLACARA.CAPTURII.DATAACCESS.Factory;


namespace DISERTATIE.FLACARA.CAPTURII.CONFIGURATION;

public static class MapperConfiguration
{
    public static IServiceCollection AddMapperConfiguration(this IServiceCollection services)
    {
        services.AddAutoMapper((serviceProvider, cfg) => {
            var dataFactory = serviceProvider.GetService<IDataFactory>();
            cfg.AddProfile(new ProfileMapper(dataFactory));
        }, AppDomain.CurrentDomain.GetAssemblies());

        return services;
    }
}
