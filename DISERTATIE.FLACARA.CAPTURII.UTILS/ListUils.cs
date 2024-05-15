using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DISERTATIE.FLACARA.CAPTURII.UTILS;

public static class ListUils<T>
{
    public static List<T> GetPage(List<T> list, int page, int pageSize)
    {
        return list.Skip(page - 1 * pageSize).Take(pageSize).ToList();
    }
}
