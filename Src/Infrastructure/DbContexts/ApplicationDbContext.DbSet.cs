
using Domain.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public partial class ApplicationDbContext
    {
        public DbSet<IdentityUser> IdentityUsers { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<ScenicSpots> ScenicSpots { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<UserFavorite> UserFavorites { get; set; }
        public DbSet<Feedback> Feedback { get; set; }
    }

}
