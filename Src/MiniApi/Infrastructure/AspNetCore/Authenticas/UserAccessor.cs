using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Applet.API.Infrastructure
{
    public class UserAccessor
    {
        public int Id { get; private set; }

        public string FullName { get; private set; }

        public string Mobile { get; private set; }

        public UserAccessor(int id, string fullName, string mobile)
        {
            Id = id;
            FullName = fullName;
            Mobile = mobile;
        }
    }
}
