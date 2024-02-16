using Juzhen.Domain.SeedWork;
using System.Threading.Tasks;

namespace Juzhen.Domain.Aggregates
{
    public interface IScenicSpotsRepository:IRepository<Users>
    {
        void Add(ScenicSpot vsion);

        void Update(ScenicSpot vsion);

        void Delete(ScenicSpot vsion);

        Task<ScenicSpot> GetAsync(int id);
    }
}
