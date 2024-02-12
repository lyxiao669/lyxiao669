using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Juzhen.Infrastructure
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> WhereIF<T>(this IQueryable<T> ts,Expression<Func<T,bool>> expression ,bool flag)
           where T : class
        {
            if (flag)
            {
                return ts.Where(expression);
            }
            return ts;
        }
        public static IQueryable<T> Page<T>(this IQueryable<T> ts,int index,int size)
            where T:class
        {
            index = (index - 1) * size;
            return ts.Skip(index)
                .Take(size);
        }
    }
}
