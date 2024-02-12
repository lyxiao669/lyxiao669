using Juzhen.Domain.Aggregates;
using Juzhen.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juzhen.Infrastructure.Repositories
{
    public class UserVsionRepository : IUserVsionRepository
    {
        readonly ApplicationDbContext _context;

        public UserVsionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public void Add(UserVsion vsion)
        {
            _context.Add(vsion);
        }

        public void Delete(UserVsion vsion)
        {
            _context.Remove(vsion);
        }

        public async Task<UserVsion> GetAsync(int id)
        {
            return await _context.UserVsions.FindAsync(id);
        }

        public void Update(UserVsion vsion)
        {
            _context.Update(vsion);
        }
    }
}
