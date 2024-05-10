using AutoMapper;
using DISERTATIE.FLACARA.CAPTURII.DATAACCESS.Data.Domains;
using DISERTATIE.FLACARA.CAPTURII.DTO.DomainsDTO;
using DISERTATIE.FLACARA.CAPTURII.DTO.EntityDTO;

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
        CreateMap<FolderWithPhotosDTO, Folder>().ReverseMap();
    }
}