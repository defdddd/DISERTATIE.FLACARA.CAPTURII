using Dapper;
using DISERTATIE.FLACARA.CAPTURII.DATAACCESS.Connection;
using DISERTATIE.FLACARA.CAPTURII.DATAACCESS.Data.Contracts;
using DISERTATIE.FLACARA.CAPTURII.DATAACCESS.Data.Domains;
using DISERTATIE.FLACARA.CAPTURII.DATAACCESS.Data.Repository;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;

namespace DISERTATIE.FLACARA.CAPTURII.DATAACCESS.Data;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(IDataAccess sqlDataAccess) : base(sqlDataAccess)
    {
        this.sqlTableName = "table_Users";
    }

    public async Task<List<dynamic>> GetTop10Users()
    {
        var sql = "SELECT TOP 10 \r\n    u.Id, \r\n    u.UserName, \r\n    AVG(r.Grade) AS AverageGrade, \r\n    COUNT(DISTINCT p.Id) AS TotalPhotos\r\nFROM \r\n    table_Users u\r\nJOIN \r\n    table_Photos p ON u.Id = p.UserId\r\nJOIN \r\n    table_Reviews r ON p.Id = r.PhotoId\r\nGROUP BY \r\n    u.Id, \r\n    u.UserName\r\nORDER BY \r\n    AverageGrade DESC, \r\n    TotalPhotos DESC;";

        using var connection = new SqlConnection(sqlDataAccess.Connection);

        connection.Open();

        var entities = await connection.QueryAsync<dynamic>(sql) ?? Enumerable.Empty<dynamic>();

        return entities.ToList();
    }

    public async Task<dynamic?> UserRankStatus(int id)
    {
        var sql = $@"SELECT 
                    u.Id, 
                    u.UserName, 
                    AVG(r.Grade) AS AverageGrade, 
                    COUNT(DISTINCT p.Id) AS TotalPhotos
                    FROM 
                        table_Users u
                    JOIN 
                        table_Photos p ON u.Id = p.UserId
                    JOIN 
                        table_Reviews r ON p.Id = r.PhotoId
                    WHERE 
                        u.Id = {id}
                    GROUP BY 
                        u.Id, 
                        u.UserName;";

        using var connection = new SqlConnection(sqlDataAccess.Connection);

        connection.Open();

        var entities = await connection.QueryFirstOrDefaultAsync<dynamic>(sql) ?? null;

        return entities;
    }
}
