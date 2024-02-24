using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Applet.API.Infrastructure
{
    public class UsersAccessor
    {
        public int Id { get;private set; }

        public string UserName { get;private set; }

        public string Password { get;private set; }
        public string Avatar { get;private set; }

        public UsersAccessor(int id, string userName, string password, string avatar)
        {
            Id = id;
            UserName = userName;
            Password = password;
            Avatar = avatar;
        }
    }
}
