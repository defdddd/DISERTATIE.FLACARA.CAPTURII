using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DISERTATIE.FLACARA.CAPTURII.DATAACCESS.Data.Repository;

public interface IRepository<T> where T : class
{
    Task<List<T>> GetAllAsync();
    Task<List<T>> GetPagedData(int pageNumber, int pageSize);
    Task<List<T>> GetByPhotoId(int photoId);
    Task<List<T>> GetEntitiesWhereAsync(Func<T, bool> expression);
    Task<T> InsertAsync(T value);
    Task<T?> UpdateAsync(T value);
    Task<T> SearchByIdAsync(int id);
    Task<T> FirstOrDefaultAsync(Func<T, bool> expression);
    Task<bool> DeleteAsync(T value);
}
