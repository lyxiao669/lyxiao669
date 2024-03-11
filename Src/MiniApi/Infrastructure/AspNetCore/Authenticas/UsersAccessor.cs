using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Applet.API.Infrastructure
{
    public class UsersAccessor
    {
        public int Id { get; private set; }
        public string UserName { get; private set; }
        public string Password { get; private set; }
        public string Avatar { get; private set; }

        public UsersAccessor(int id, string userName,  string avatar)
        {
            Id = id;
            UserName = userName;
            // Password = password;
            Avatar = avatar;
        }
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UsersAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

    }

}
