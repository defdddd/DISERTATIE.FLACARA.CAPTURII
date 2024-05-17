using DISERTATIE.FLACARA.CAPTURII.DATAACCESS.Connection;
using DISERTATIE.FLACARA.CAPTURII.DATAACCESS.Data.Contracts;
using DISERTATIE.FLACARA.CAPTURII.DATAACCESS.Data.Domains;
using DISERTATIE.FLACARA.CAPTURII.DATAACCESS.Data.Repository;

namespace DISERTATIE.FLACARA.CAPTURII.DATAACCESS.Data;

public class PhotoRepository : Repository<Photo>, IPhotoRepository
{
    public PhotoRepository(IDataAccess sqlDataAccess) : base(sqlDataAccess)
    {
        this.sqlTableName = "table_Photos";
    }

}
