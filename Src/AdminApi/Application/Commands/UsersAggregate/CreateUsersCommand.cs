using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminApi.Application
{
    public class CreateUsersCommand: IRequest<bool>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Avatar { get; set; }
    }
}
