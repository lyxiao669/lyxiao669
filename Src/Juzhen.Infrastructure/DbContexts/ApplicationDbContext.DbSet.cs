
using Juzhen.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace Juzhen.Infrastructure
{
    public partial class ApplicationDbContext
    {
        public DbSet<IdentityUser> IdentityUsers { get; set; }

        public DbSet<AnswerResultRecord> AnswerResultRecords { get; set; }

        public DbSet<Achievement> Achievements { get; set; }

        public DbSet<Question> Questions { get; set; }

        public DbSet<QuestionOptions> QuestionOptions { get; set; }

        public DbSet<Banner> Banners { get; set; }


        public DbSet<QuestionBank> QuestionBanks { get; set; }

        public DbSet<UserVsion> UserVsions { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<ScenicSpot> ScenicSpots { get; set; }

    }
    
}
