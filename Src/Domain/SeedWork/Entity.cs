using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Domain.SeedWork
{
    public abstract class Entity: IEntity
    {
        private int? _requestedHashCode;

        
        private int _id;
       
        private readonly List<INotification> _notifications = new List<INotification>();
        [System.Text.Json.Serialization.JsonIgnore]
        public IReadOnlyList<INotification> DomainEvents => _notifications.AsReadOnly();

        public virtual int Id
        {
            get
            {
                return _id;
            }
            protected set
            {
                _id = value;
            }
        }
      
        public void AddDominEvent(INotification notification)
        {
            _notifications.Add(notification);
        }

        public void ClearDomainEvents()
        {
            _notifications.Clear();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Entity))
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            if (GetType() != obj.GetType())
                return false;

            Entity item = (Entity)obj;

            if (item.IsTransient() || IsTransient())
                return false;
            else
                return item.Id == Id;
        }

        public override int GetHashCode()
        {
            if (!IsTransient())
            {
                // XOR for random distribution (http://blogs.msdn.com/b/ericlippert/archive/2011/02/28/guidelines-and-rules-for-gethashcode.aspx)
                if (!_requestedHashCode.HasValue)
                    _requestedHashCode = Id.GetHashCode() ^ 31;

                return _requestedHashCode.Value;
            }
            else
                return base.GetHashCode();
        }

        public bool IsTransient()
        {
            return Id == default;
        }

        public static bool operator ==(Entity left, Entity right)
        {
            return EqualityComparer<Entity>.Default.Equals(left, right);
        }

        public static bool operator !=(Entity left, Entity right)
        {
            return !(left == right);
        }
    }
}
