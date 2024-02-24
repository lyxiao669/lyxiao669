
using Juzhen.IdentityUI;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class DefaultIdentityUIDbContext : IdentityUIDbContext
    {
        public DefaultIdentityUIDbContext(DbContextOptions<DefaultIdentityUIDbContext> options)
            : base(options)
        {

        }

        public DbSet<__IdentityUser> Users { get; set; }
    }
}
