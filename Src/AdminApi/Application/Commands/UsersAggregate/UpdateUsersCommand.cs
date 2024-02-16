using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminApi.Application
{
    public class UpdateUsersCommand : IRequest<bool>
    {
        public int Id { get; set; } 
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Avatar { get; set; }

        public UpdateUsersCommand( string userName, string password, string avatar)
        {
            
            UserName = userName;
            Password = password;
            Avatar = avatar;
        }
    }
}
