using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminAPI.Infrastructure.Services
{
    public interface IIdWorker
    {
        long NextId();
        string NextIdString();
        string NextIdString(string prefix);
    }
}
