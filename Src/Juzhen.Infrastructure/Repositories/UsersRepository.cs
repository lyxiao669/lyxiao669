using Juzhen.Domain.Aggregates;
using Juzhen.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juzhen.Infrastructure.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        readonly ApplicationDbContext _context;

        public UsersRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public void Add(Users users)
        {
            _context.Add(users);
        }

        public void Delete(Users users)
        {
            _context.Remove(users);
        }

        public async Task<Users> GetAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public void Update(Users users)
        {
            _context.Update(users);
        }
    }
}
