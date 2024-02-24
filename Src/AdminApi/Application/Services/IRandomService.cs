
using Infrastructure;
using System.Threading.Tasks;

namespace AdminApi.Application
{
    public interface IRandomService
    {
        string RandomNumber(int dight);
    }

    public class RandomService : IRandomService
    {
        readonly ApplicationDbContext _context;

        public RandomService(ApplicationDbContext context)
        {
            _context = context;
        }

        public string RandomNumber(int dight)
        {
            var number = RandomUtil.GenerateNumber(dight);

            return number;
        }

    }
}
