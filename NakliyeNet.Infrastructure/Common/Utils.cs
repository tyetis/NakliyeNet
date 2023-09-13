using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NakliyeNet.Infrastructure.Common
{
    public static class Utils
    {
        public static IQueryable<T> ApplyPagination<T>(this IQueryable<T> query, int pageSize, int currentPage)
        {
            return query.Skip(currentPage * pageSize).Take(pageSize);
        }
    }
}
