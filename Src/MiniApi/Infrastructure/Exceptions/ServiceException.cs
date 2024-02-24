using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Juzhen.MiniProgramAPI
{
    public class ServiceException : Exception
    {
        public ServiceException(string message)
            : this(message, 500)
        {

        }
      
        public ServiceException(string message, int statusCode)
          : base(message)
        {
            StatusCode = statusCode;
        }

        public int StatusCode { get; set; }
    }
}
