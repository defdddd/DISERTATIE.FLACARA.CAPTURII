using DISERTATIE.FLACARA.CAPTURII.DTO.DomainsDTO;
using DISERTATIE.FLACARA.CAPTURII.DTO.EntityDTO;

namespace DISERTATIE.FLACARA.CAPTURII.SERVICES.Contracts;

public interface IPhotoService : IBaseService<PhotoDTO>
{
    Task<List<PhotoDTO>> GetPosts(int page, int pageSize); 
    Task<object> GetTop10Posts();
    Task<List<PhotoDTO>> GetUsersPosts(int page, int pageSize, string lastName);
    Task<dynamic> GetTop10UsersPosts(int userId);
}
