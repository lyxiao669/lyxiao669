using Juzhen.Domain.Exceptions;

namespace Juzhen.Domain.SeedWork
{
    public abstract class AggregateRoot : Entity, IAggregateRoot
    {
        public void ThrowDomainException(string message)
        {
            throw new DomainException(GetType().Name, message);
        }
    }
}
