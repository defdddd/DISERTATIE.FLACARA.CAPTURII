using DISERTATIE.FLACARA.CAPTURII.DATAACCESS.Connection;
using DISERTATIE.FLACARA.CAPTURII.DATAACCESS.Data.Contracts;
using DISERTATIE.FLACARA.CAPTURII.DATAACCESS.Data.Domains;
using DISERTATIE.FLACARA.CAPTURII.DATAACCESS.Data.Repository;

namespace DISERTATIE.FLACARA.CAPTURII.DATAACCESS.Data;

public class ReviewRepository : Repository<Review>, IReviewRepository
{
    public ReviewRepository(IDataAccess sqlDataAccess) : base(sqlDataAccess)
    {
    }
}
