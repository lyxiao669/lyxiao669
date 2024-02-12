
using System.Collections.Generic;

namespace AdminApi.Application
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
