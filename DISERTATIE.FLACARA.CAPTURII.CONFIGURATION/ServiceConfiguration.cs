using AutoMapper;
using DISERTATIE.FLACARA.CAPTURII.DATAACCESS.Factory;
using DISERTATIE.FLACARA.CAPTURII.SERVICES;
using DISERTATIE.FLACARA.CAPTURII.SERVICES.Authentification;
using DISERTATIE.FLACARA.CAPTURII.SERVICES.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DISERTATIE.FLACARA.CAPTURII.CONFIGURATION;

public static class ServiceConfiguration
{
    public static IServiceCollection AddServiceConfiguration(this IServiceCollection services, IConfiguration config)
    {
        services.AddTransient<ICommentService, CommentService>();

        services.AddTransient<IPhotoService, PhotoService>();

        services.AddTransient<IReviewService, ReviewService>();

        services.AddTransient<IUserService, UserService>();

        services.AddTransient<IServiceAuthentification>
            (
               provider => new ServiceAuthentication
                            (
                               provider.GetService<IDataFactory>(),
                               config.GetConnectionString("MySecretKey"),
                               provider.GetService<IMapper>()
                            )
            );

        return services;
    }
}
