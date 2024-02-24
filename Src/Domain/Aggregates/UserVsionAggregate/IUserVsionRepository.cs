using Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Aggregates
{
    public interface IUserVsionRepository:IRepository<UserVsion>
    {
        void Add(UserVsion vsion);

        void Update(UserVsion vsion);

        void Delete(UserVsion vsion);

        Task<UserVsion> GetAsync(int id);
    }
}
