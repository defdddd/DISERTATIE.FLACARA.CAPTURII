using DISERTATIE.FLACARA.CAPTURII.DATAACCESS.Data.Contracts;
using DISERTATIE.FLACARA.CAPTURII.DATAACCESS.Data;
using System;
using DISERTATIE.FLACARA.CAPTURII.DATAACCESS.Connection;

namespace DISERTATIE.FLACARA.CAPTURII.DATAACCESS.Factory;

public class DataFactory : IDataFactory
{
    private readonly IDataAccess sqlDataAccess;

    public IReviewRepository ReviewRepository => new ReviewRepository(sqlDataAccess);

    public IUserRepository UserRepository => new UserRepository(sqlDataAccess);

    public ICommentRepository CommentRepository => new CommentRepository(sqlDataAccess);

    public IPhotoRepository PhotoRepository => new PhotoRepository(sqlDataAccess);

    public DataFactory(IDataAccess sqlDataAccess)
    {
        this.sqlDataAccess = sqlDataAccess;
    }
}
