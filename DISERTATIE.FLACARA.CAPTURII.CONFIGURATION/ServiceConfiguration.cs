using AutoMapper;
using DISERTATIE.FLACARA.CAPTURII.DATAACCESS.Factory;
using DISERTATIE.FLACARA.CAPTURII.SERVICES;
using DISERTATIE.FLACARA.CAPTURII.SERVICES.Authentification;
using DISERTATIE.FLACARA.CAPTURII.SERVICES.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DISERTATIE.FLACARA.CAPTURII.CONFIGURATION;

public static class ServiceConfiguration
{
    public static IServiceCollection AddServiceConfiguration(this IServiceCollection services, IConfiguration config)
    {
        services.AddScoped<ICommentService, CommentService>();

        services.AddScoped<IPhotoService, PhotoService>();

        services.AddScoped<IReviewService, ReviewService>();

        services.AddScoped<IFolderService, FolderService>();

        services.AddScoped<IUserService, UserService>();

        services.AddScoped<IServiceAuthentification>
            (
               provider => new ServiceAuthentication
                            (
                               provider.GetService<IDataFactory>(),
                               config.GetConnectionString("MySecretKey"),
                               provider.GetService<IMapper>()
                            )
            );

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = "JwtBearer";
            options.DefaultChallengeScheme = "JwtBearer";
        })
                .AddJwtBearer("JwtBearer", jwtBearerOptions =>
                {
                    jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetConnectionString("MySecretKey"))),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.FromMinutes(5)
                    };
                });

        return services;
    }
}
