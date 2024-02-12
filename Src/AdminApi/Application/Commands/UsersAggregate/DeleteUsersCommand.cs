using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminApi.Application
{
    public class DeleteUsersCommand : IRequest<bool>
    {
        public int Id { get; set; }

        
    }
}
