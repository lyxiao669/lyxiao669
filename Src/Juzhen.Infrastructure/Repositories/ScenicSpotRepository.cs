using Juzhen.Domain.Aggregates;
using Juzhen.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Juzhen.Infrastructure.Repositories
{
    public class ScenicSpotRepository : IScenicSpotsRepository
    {
        readonly ApplicationDbContext _context;

        public ScenicSpotRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public void Add(ScenicSpot scenicSpot)
        {
            _context.Add(scenicSpot);
        }

        public void Delete(ScenicSpot scenicSpot)
        {
            _context.Remove(scenicSpot);
        }

        public async Task<ScenicSpot> GetAsync(int id)
        {
            return await _context.ScenicSpots.FindAsync(id);
        }

        public void Update(ScenicSpot ScenicSpot)
        {
            _context.Update(ScenicSpot);
        }
    }
}
