using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Juzhen.Infrastructure
{
    public interface IIdWorker
    {
        long NextId();
        string NextIdString();
        string NextIdString(string prefix);
    }
}
