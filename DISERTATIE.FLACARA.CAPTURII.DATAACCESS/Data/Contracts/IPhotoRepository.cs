using DISERTATIE.FLACARA.CAPTURII.DATAACCESS.Data.Domains;
using DISERTATIE.FLACARA.CAPTURII.DATAACCESS.Data.Repository;

namespace DISERTATIE.FLACARA.CAPTURII.DATAACCESS.Data.Contracts;

public interface IPhotoRepository : IRepository<Photo>
{
    Task<List<dynamic>> GetTop10Posts();
    Task<List<Photo>> GetPagedData(int pageNumber, int pageSize, string lastName);
    Task<List<dynamic>> GetTop10UsersPosts(int userId);

}