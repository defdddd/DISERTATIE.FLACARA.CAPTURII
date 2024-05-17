using AutoMapper;
using DISERTATIE.FLACARA.CAPTURII.DATAACCESS.Data;
using DISERTATIE.FLACARA.CAPTURII.DATAACCESS.Data.Contracts;
using DISERTATIE.FLACARA.CAPTURII.DATAACCESS.Data.Domains;
using DISERTATIE.FLACARA.CAPTURII.DATAACCESS.Factory;
using DISERTATIE.FLACARA.CAPTURII.DTO.DomainsDTO;
using DISERTATIE.FLACARA.CAPTURII.DTO.EntityDTO;
using DISERTATIE.FLACARA.CAPTURII.MAPPERS.Resolvers;

namespace DISERTATIE.FLACARA.CAPTURII.MAPPERS;

public class ProfileMapper : Profile
{
    private readonly IDataFactory repositories;

    public ProfileMapper(IDataFactory repositories)
    {
        this.repositories = repositories;

        ConfigureUserMappings();
        ConfigureCommentMappings();
        ConfigureReviewMappings();
        ConfigurePhotoMappings();
        ConfigureFolderMappings();
        ConfigureUserProfileMappings();
        ConfigureRegisterUserMappings();
        ConfigureFolderWithPhotosMappings();
    }

    private void ConfigureUserMappings()
    {
        CreateMap<UserDTO, User>().ReverseMap();
    }

    private void ConfigureCommentMappings()
    {
        CreateMap<CommentDTO, Comment>();

        CreateMap<Comment, CommentDTO>()
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => UserResolver.Resolve(this.repositories, src.UserId)));
    }

    private void ConfigureReviewMappings()
    {
        CreateMap<ReviewDTO, Review>();

        CreateMap<Review, ReviewDTO>()
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => UserResolver.Resolve(this.repositories, src.UserId)));
    }

    private void ConfigurePhotoMappings()
    {
        CreateMap<PhotoDTO, Photo>();

        CreateMap<Photo, PhotoDTO>()
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => UserResolver.Resolve(this.repositories, src.UserId)));

    }

    private void ConfigureFolderMappings()
    {
        CreateMap<FolderDTO, Folder>().ReverseMap();
    }

    private void ConfigureUserProfileMappings()
    {
        CreateMap<UserProfileDTO, User>().ReverseMap();
    }

    private void ConfigureRegisterUserMappings()
    {
        CreateMap<RegisterUserDTO, User>().ReverseMap();
    }

    private void ConfigureFolderWithPhotosMappings()
    {
        CreateMap<FolderWithPhotosDTO, Folder>().ReverseMap();
    }
}