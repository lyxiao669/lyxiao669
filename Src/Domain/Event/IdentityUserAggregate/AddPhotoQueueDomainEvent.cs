using Domain.Aggregates;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Event
{
    public class AddPhotoQueueDomainEvent : INotification
    {
        public IdentityUser IdentityUser { get; set; }
        public AddPhotoQueueDomainEvent(IdentityUser identityUser)
        {
            IdentityUser = identityUser;
        }
    }
}
