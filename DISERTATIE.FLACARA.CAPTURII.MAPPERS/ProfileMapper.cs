using AutoMapper;
using DISERTATIE.FLACARA.CAPTURII.DATAACCESS.Data.Domains;
using DISERTATIE.FLACARA.CAPTURII.DTO;

namespace DISERTATIE.FLACARA.CAPTURII.MAPPERS;

public class ProfileMapper : Profile
{
    public ProfileMapper()
    {
        //DOMAINS
        CreateMap<UserDTO, User>().ReverseMap();
        CreateMap<CommentDTO, Comment>().ReverseMap();
        CreateMap<ReviewDTO, Review>().ReverseMap();
        CreateMap<PhotoDTO, Photo>().ReverseMap();
        CreateMap<FolderDTO, Folder>().ReverseMap();


        //OTHERS DTOS
        CreateMap<UserProfileDTO, User>().ReverseMap();
        CreateMap<RegisterUserDTO, User>().ReverseMap();

        //FUNCS
        CreateMap<Func<UserDTO, bool>, Func<User, bool>>().ReverseMap();
        CreateMap<Func<PhotoDTO, bool>, Func<Photo, bool>>().ReverseMap();
        CreateMap<Func<ReviewDTO, bool>, Func<Review, bool>>().ReverseMap();
        CreateMap<Func<CommentDTO, bool>, Func<Comment, bool>>().ReverseMap();

    }
}