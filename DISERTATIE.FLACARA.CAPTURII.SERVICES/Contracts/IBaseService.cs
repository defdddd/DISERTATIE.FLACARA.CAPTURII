using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
///
namespace DISERTATIE.FLACARA.CAPTURII.SERVICES.Contracts;

public interface IBaseService<T> where T : class
{
    Task<List<T>> Entities();
    Task<List<T>> EntitiesWithPagination(int page, int pageSize);
    Task<T> InsertEntityAsync(T value);
    Task<T> UpdateEntityAsync(T value);
    Task<T> SearchEntityByIdAsync(int id);
    Task<bool> DeleteEntityAsync(int id);
}