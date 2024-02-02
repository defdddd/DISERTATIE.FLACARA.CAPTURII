using DISERTATIE.FLACARA.CAPTURII.MAPPERS;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;


namespace DISERTATIE.FLACARA.CAPTURII.CONFIGURATION;

public static class MapperConfiguration
{
    public static IServiceCollection AddMapperConfiguration(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(ProfileMapper));

        return services;
    }
}
