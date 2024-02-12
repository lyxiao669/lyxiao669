using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Juzhen.Domain.SeedWork
{
    public interface IUnitOfWork
    {
        Task SaveEntitiesAsync(CancellationToken cancellationToken = default);

        Task<bool> SaveChangeAsync(CancellationToken cancellationToken = default);
    }
}
