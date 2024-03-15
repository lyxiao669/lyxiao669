using Domain.Aggregates;
using Domain.SeedWork;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UserFavoriteRepository : IUserFavoriteRepository
    {
        private readonly ApplicationDbContext _context;

        public UserFavoriteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public void Add(UserFavorite userFavorite)
        {
            _context.UserFavorites.Add(userFavorite);
        }

        public void Delete(UserFavorite userFavorite)
        {
            _context.UserFavorites.Remove(userFavorite);
        }

        public async Task<UserFavorite> GetAsync(int id)
        {
            return await _context.UserFavorites.FindAsync(id);
        }

        public void Update(UserFavorite userFavorite)
        {
            _context.UserFavorites.Update(userFavorite);
        }
    }
}
