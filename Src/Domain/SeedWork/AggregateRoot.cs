using Domain.Exceptions;

namespace Domain.SeedWork
{
    public abstract class AggregateRoot : Entity, IAggregateRoot
    {
        public void ThrowDomainException(string message)
        {
            throw new DomainException(GetType().Name, message);
        }
    }
}
