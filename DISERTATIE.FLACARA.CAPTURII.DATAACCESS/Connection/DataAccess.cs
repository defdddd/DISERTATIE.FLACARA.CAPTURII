using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DISERTATIE.FLACARA.CAPTURII.DATAACCESS.Connection;

public class DataAccess : IDataAccess
{
    public string Connection { get; private set; }
    public DataAccess(string connection)
    {
        Connection = connection;
    }
}
