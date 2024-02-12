using Juzhen.Domain.Aggregates;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juzhen.Domain.Event
{
    public class PhotoDomainEvent:INotification
    {

        public IdentityUser IdentityUser{ get; set; }
        public PhotoDomainEvent(IdentityUser identityUser)
        {
            IdentityUser = identityUser;
        }

    }
}
