using Dapper;
using DISERTATIE.FLACARA.CAPTURII.DATAACCESS.Connection;
using DISERTATIE.FLACARA.CAPTURII.DATAACCESS.Data.Contracts;
using DISERTATIE.FLACARA.CAPTURII.DATAACCESS.Data.Domains;
using DISERTATIE.FLACARA.CAPTURII.DATAACCESS.Data.Repository;
using System.Data.SqlClient;

namespace DISERTATIE.FLACARA.CAPTURII.DATAACCESS.Data;

public class PhotoRepository : Repository<Photo>, IPhotoRepository
{
    public PhotoRepository(IDataAccess sqlDataAccess) : base(sqlDataAccess)
    {
        this.sqlTableName = "table_Photos";
    }

    public async Task<List<Photo>> GetPagedData(int pageNumber, int pageSize, string lastName)
    {
        using var connection = new SqlConnection(sqlDataAccess.Connection);

        connection.Open();

        var sql = $@"
            SELECT p.*
            FROM table_Photos p
            JOIN table_Users u ON p.UserId = u.Id
            WHERE u.LastName = '{lastName}'
            ORDER BY p.Id
            OFFSET {(pageNumber - 1) * pageSize} ROWS
            FETCH NEXT {pageSize} ROWS ONLY;";

        var entities = await connection.QueryAsync<Photo>(sql) ?? Enumerable.Empty<Photo>();

        return entities.ToList();
    }

    public async Task<List<dynamic>> GetTop10Posts()
    {
        var sql = "SELECT TOP 10\r\n    p.Id AS PhotoId,\r\n    p.URL AS PhotoURL,\r\n    AVG(r.Grade) AS AverageGrade\r\nFROM\r\n    table_Photos p\r\nJOIN\r\n    table_Reviews r ON p.Id = r.PhotoId\r\nGROUP BY\r\n    p.Id, p.URL\r\nORDER BY\r\n    AverageGrade DESC;";

        using var connection = new SqlConnection(sqlDataAccess.Connection);

        connection.Open();

        var entities = await connection.QueryAsync<dynamic>(sql) ?? Enumerable.Empty<dynamic>();

        return entities.ToList();
    }

}
