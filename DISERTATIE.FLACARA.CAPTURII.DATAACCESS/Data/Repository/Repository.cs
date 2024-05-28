using DISERTATIE.FLACARA.CAPTURII.DATAACCESS.Connection;
using Dapper.Contrib.Extensions;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using Dapper;
using static Dapper.Contrib.Extensions.SqlMapperExtensions;
using System.Reflection;
using DISERTATIE.FLACARA.CAPTURII.DATAACCESS.Data.Domains;

namespace DISERTATIE.FLACARA.CAPTURII.DATAACCESS.Data.Repository;

public abstract class Repository<T> : IRepository<T> where T : class
{
    #region Fields
    protected IDataAccess sqlDataAccess;
    protected string sqlTableName;
    #endregion

    #region constructors
    public Repository(IDataAccess sqlDataAccess)
    {
        this.sqlDataAccess = sqlDataAccess ?? throw new NullReferenceException();
    }

    #endregion

    #region Methods
    public async Task<bool> DeleteAsync(T value)
    {
        using var connection = new SqlConnection(sqlDataAccess.Connection);

        return await connection.DeleteAsync(value);
    }

    public async Task<List<T>> GetAllAsync()
    {
        using var connection = new SqlConnection(sqlDataAccess.Connection);

        var entities = await connection.GetAllAsync<T>() ?? Enumerable.Empty<T>();

        return entities.ToList();
    }

    public async Task<T> InsertAsync(T value)
    {
        using var connection = new SqlConnection(sqlDataAccess.Connection);

        var id = await connection.InsertAsync(value);

        return await connection.GetAsync<T>(id);
    }

    public async Task<T> SearchByIdAsync(int id)
    {
        using var connection = new SqlConnection(sqlDataAccess.Connection);
        

        return await connection.GetAsync<T>(id);
    }


    public async Task<List<T>> GetPagedData(int pageNumber, int pageSize)
    {
        using var connection = new SqlConnection(sqlDataAccess.Connection);

        connection.Open();

        var sql = $@"
            SELECT * 
            FROM [{sqlTableName}]
            ORDER BY [Id] DESC
            OFFSET {(pageNumber - 1) * pageSize} ROWS 
            FETCH NEXT {pageSize} ROWS ONLY;";

        var entities = await connection.QueryAsync<T>(sql) ?? Enumerable.Empty<T>();

        return entities.ToList();
    }

    public async Task<List<T>> GetByPhotoId(int photoId)
    {
        using var connection = new SqlConnection(sqlDataAccess.Connection);

        connection.Open();

        var sql = $@"
                SELECT * 
                FROM [{sqlTableName}]
                WHERE [PhotoId] = {photoId};";

        var entities = await connection.QueryAsync<T>(sql) ?? Enumerable.Empty<T>();

        return entities.ToList();
    }

    public async Task<T?> UpdateAsync(T value)
    {
        using var connection = new SqlConnection(sqlDataAccess.Connection);

        if (await connection.UpdateAsync(value))
            return value;

        return null;
    }

    public async Task<List<T>> GetEntitiesWhereAsync(Func<T, bool> expression)
    {
        using var connection = new SqlConnection(sqlDataAccess.Connection);

        var entities = await connection.GetAllAsync<T>() ?? Enumerable.Empty<T>();

        return entities.Where(expression).ToList();
    }

    public async Task<T> FirstOrDefaultAsync(Func<T, bool> expression)
    {
        using var connection = new SqlConnection(sqlDataAccess.Connection);

        var entities = await connection.GetAllAsync<T>() ?? Enumerable.Empty<T>();

        return entities.FirstOrDefault(expression);
    }
    #endregion
}
