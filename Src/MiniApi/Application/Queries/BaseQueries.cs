
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniApi.Application
{
    public abstract class BaseQueries
    {
        protected PageResult<T> PageResult<T>(IEnumerable<T> data, int total)
        {
            return new PageResult<T>(data, total);
        }
        protected PageResult<T> PageResult<T>(IEnumerable<T> data)
        {
            return new PageResult<T>(data);
        }
    }
}
