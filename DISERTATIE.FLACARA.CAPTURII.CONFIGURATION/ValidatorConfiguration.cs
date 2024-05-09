using DISERTATIE.FLACARA.CAPTURII.DTO;
using DISERTATIE.FLACARA.CAPTURII.VALIDATORS.DTO.Validation;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace DISERTATIE.FLACARA.CAPTURII.CONFIGURATION;


public static class ValidatorConfiguration
{
    public static IServiceCollection ValidationConfiguration(this IServiceCollection services)
    {
        services.AddScoped<IValidator<UserDTO>, UserValidator>();
        services.AddScoped<IValidator<PhotoDTO>, PhotoValidator>();
        services.AddScoped<IValidator<ReviewDTO>, ReviewValidator>();
        services.AddScoped<IValidator<CommentDTO>, CommentValidator>();
        services.AddScoped<IValidator<FolderDTO>, FolderValidator>();


        return services;
    }
}