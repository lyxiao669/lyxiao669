using Juzhen.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juzhen.Domain.Aggregates
{
    public interface IAchievementRepository:IRepository<Achievement>
    {
        void Add(Achievement achievement);

        void Update(Achievement achievement);

        void Delete(Achievement achievement);

        Task<Achievement> GetAsync(int id);
    }
}
