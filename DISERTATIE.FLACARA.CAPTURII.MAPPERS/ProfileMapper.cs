using AutoMapper;
using DISERTATIE.FLACARA.CAPTURII.DATAACCESS.Data.Domains;
using DISERTATIE.FLACARA.CAPTURII.DTO;

namespace DISERTATIE.FLACARA.CAPTURII.MAPPERS;

public class ProfileMapper : Profile
{
    public ProfileMapper()
    {
        CreateMap<UserDTO, User>().ReverseMap();
        CreateMap<RegisterUserDTO, User>().ReverseMap();
        CreateMap<CommentDTO, Comment>().ReverseMap();
        CreateMap<ReviewDTO, Review>().ReverseMap();
        CreateMap<PhotoDTO, Photo>().ReverseMap();
    }
}