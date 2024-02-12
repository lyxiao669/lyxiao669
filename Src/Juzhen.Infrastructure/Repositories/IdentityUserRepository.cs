using Juzhen.Domain.Aggregates;
using Juzhen.Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juzhen.Infrastructure.Repositories
{
    public class IdentityUserRepository : IIdentityUserRepository
    {
        readonly ApplicationDbContext _context;

        public IdentityUserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public void Add(IdentityUser user)
        {
            _context.IdentityUsers.Add(user);
        }

        public void Delete(IdentityUser user)
        {
            _context.IdentityUsers.Remove(user);
        }

        public async Task<IdentityUser> GetAsync(int id)
        {
            return await _context.IdentityUsers.FindAsync(id);
        }

        public async Task<List<IdentityUser>> GetNotQrCodeImgList()
        {
            return await _context.IdentityUsers.Where(a=>a.IsQrCode==false).ToListAsync();
        }

        public void Update(IdentityUser user)
        {
            _context.IdentityUsers.Update(user);
        }
    }
}
