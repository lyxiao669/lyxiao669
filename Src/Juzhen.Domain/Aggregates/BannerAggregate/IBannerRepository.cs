using Juzhen.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juzhen.Domain.Aggregates
{
    public interface IBannerRepository:IRepository<Banner>
    {
        void Add(Banner banner);

        void Update(Banner banner);

        void Delete(Banner banner);

        Task<Banner> GetAsync(int id);
    }
}
