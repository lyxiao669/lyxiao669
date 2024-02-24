using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.SeedWork
{
    public interface IRepository
    {

    }
    public interface IRepository<T> 
        : IRepository where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
