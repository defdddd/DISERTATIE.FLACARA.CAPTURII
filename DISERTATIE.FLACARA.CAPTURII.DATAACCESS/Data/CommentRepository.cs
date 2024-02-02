using DISERTATIE.FLACARA.CAPTURII.DATAACCESS.Connection;
using DISERTATIE.FLACARA.CAPTURII.DATAACCESS.Data.Contracts;
using DISERTATIE.FLACARA.CAPTURII.DATAACCESS.Data.Domains;
using DISERTATIE.FLACARA.CAPTURII.DATAACCESS.Data.Repository;


namespace DISERTATIE.FLACARA.CAPTURII.DATAACCESS.Data;

public class CommentRepository : Repository<Comment>, ICommentRepository
{
    public CommentRepository(IDataAccess sqlDataAccess) : base(sqlDataAccess)
    {
    }
}
