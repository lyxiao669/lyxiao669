using Juzhen.Domain.Aggregates;
using Juzhen.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juzhen.Infrastructure.Repositories
{
    public class BannerRepository : IBannerRepository
    {
        readonly ApplicationDbContext _context;

        public BannerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public void  Add(Banner banner)
        {
            _context.Banners.Add(banner);
        }

        public void Delete(Banner banner)
        {
            _context.Banners.Remove(banner);
        }

        public async Task<Banner> GetAsync(int id)
        {
            return await _context.Banners.FindAsync(id);
        }

        public void Update(Banner banner)
        {
            _context.Banners.Update(banner);
        }
    }
}
