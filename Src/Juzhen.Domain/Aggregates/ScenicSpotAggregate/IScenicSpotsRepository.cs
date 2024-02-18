using Juzhen.Domain.SeedWork;
using System.Threading.Tasks;

namespace Juzhen.Domain.Aggregates
{
    // public interface IScenicSpotsRepository : IRepository<ScenicSpot>
    public interface IScenicSpotsRepository : IRepository<ScenicSpots>
    {
        void Add(ScenicSpots scenicSpot);

        void Update(ScenicSpots scenicSpot);

        void Delete(ScenicSpots scenicSpot);

        Task<ScenicSpots> GetAsync(int id);
    }
}
