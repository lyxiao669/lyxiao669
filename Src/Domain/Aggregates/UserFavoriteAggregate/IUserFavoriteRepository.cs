using Domain.SeedWork;
using System.Threading.Tasks;

namespace Domain.Aggregates
{
    public interface IUserFavoriteRepository : IRepository<UserFavorite>
    {
        void Add(UserFavorite userFavorite);

        void Update(UserFavorite userFavorite);

        void Delete(UserFavorite userFavorite);

        Task<UserFavorite> GetAsync(int id);
        void IsFavorite(UserFavorite userFavorite);

    }
}
