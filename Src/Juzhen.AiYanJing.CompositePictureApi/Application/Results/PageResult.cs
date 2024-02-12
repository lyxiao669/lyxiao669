using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Juzhen.AiYanJing.CompositePictureApi
{
    public class PageResult<T>
    {
        public IEnumerable<T> Data { get; }
        public long Total { get; set; }
        public PageResult(IEnumerable<T> data, int total)
        {
            Data = data;
            Total = total;
        }
        public PageResult(IEnumerable<T> data)
        {
            Data = data;
            Total = data.Count();
        }
    }
}
