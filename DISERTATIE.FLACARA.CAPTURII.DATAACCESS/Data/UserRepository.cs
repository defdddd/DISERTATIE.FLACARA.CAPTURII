using DISERTATIE.FLACARA.CAPTURII.DATAACCESS.Connection;
using DISERTATIE.FLACARA.CAPTURII.DATAACCESS.Data.Contracts;
using DISERTATIE.FLACARA.CAPTURII.DATAACCESS.Data.Domains;
using DISERTATIE.FLACARA.CAPTURII.DATAACCESS.Data.Repository;

namespace DISERTATIE.FLACARA.CAPTURII.DATAACCESS.Data;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(IDataAccess sqlDataAccess) : base(sqlDataAccess)
    {
        this.sqlTableName = "table_Users";
    }
}
