using Juzhen.IdentityUI.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure
{
    [Table("__identity_user")]
    public class __IdentityUser
    {
        public int Id { get; set; }
        public string HeadImg { get; set; }
        public string Mobile { get; set; }
        public string Name { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
        public virtual IdentityRole Role { get; set; }
    }
}
