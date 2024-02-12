using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juzhen.Domain.Exceptions
{
    public class DomainException : Exception
    {
        public string Domain { get; private set; }
     
        public DomainException(string domain, string message)
            : base(message)
        {
            Domain = domain;
        }
    }
}
