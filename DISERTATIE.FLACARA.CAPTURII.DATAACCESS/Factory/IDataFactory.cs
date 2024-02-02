using DISERTATIE.FLACARA.CAPTURII.DATAACCESS.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DISERTATIE.FLACARA.CAPTURII.DATAACCESS.Factory;

public interface IDataFactory
{
    IUserRepository UserRepository { get; }
    ICommentRepository CommentRepository { get; }
    IReviewRepository ReviewRepository { get; }
    IPhotoRepository PhotoRepository { get; }
}
