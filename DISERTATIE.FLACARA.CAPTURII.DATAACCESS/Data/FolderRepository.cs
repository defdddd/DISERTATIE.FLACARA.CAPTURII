using DISERTATIE.FLACARA.CAPTURII.DATAACCESS.Connection;
using DISERTATIE.FLACARA.CAPTURII.DATAACCESS.Data.Contracts;
using DISERTATIE.FLACARA.CAPTURII.DATAACCESS.Data.Domains;
using DISERTATIE.FLACARA.CAPTURII.DATAACCESS.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DISERTATIE.FLACARA.CAPTURII.DATAACCESS.Data;

public class FolderRepository : Repository<Folder>, IFolderRepository
{
    public FolderRepository(IDataAccess sqlDataAccess) : base(sqlDataAccess)
    {
        this.sqlTableName = "table_Folder";
    }
}