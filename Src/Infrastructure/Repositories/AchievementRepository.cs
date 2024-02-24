using Domain.Aggregates;
using Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class AchievementRepository : IAchievementRepository
    {
        readonly ApplicationDbContext _context;

        public AchievementRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public void Add(Achievement achievement)
        {
            _context.Achievements.Add(achievement);
        }

        public void Delete(Achievement achievement)
        {
            _context.Achievements.Remove(achievement);
        }

        public async Task<Achievement> GetAsync(int id)
        {
            return await _context.Achievements.FindAsync(id);
        }

        public void Update(Achievement achievement)
        {
            _context.Achievements.Update(achievement);
        }
    }
}
