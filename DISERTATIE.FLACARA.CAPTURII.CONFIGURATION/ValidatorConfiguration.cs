using DISERTATIE.FLACARA.CAPTURII.DTO;
using DISERTATIE.FLACARA.CAPTURII.VALIDATORS.DTO.Validation;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace DISERTATIE.FLACARA.CAPTURII.CONFIGURATION;


public static class ValidatorConfiguration
{
    public static IServiceCollection ValidationConfiguration(this IServiceCollection services)
    {
        services.AddTransient<IValidator<UserDTO>, UserValidator>();
        services.AddTransient<IValidator<PhotoDTO>, PhotoValidator>();
        services.AddTransient<IValidator<ReviewDTO>, ReviewValidator>();
        services.AddTransient<IValidator<CommentDTO>, CommentValidator>();

        return services;
    }
}