using Juzhen.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juzhen.Domain.Aggregates
{
    public interface IUsersRepository:IRepository<Users>
    {
        void Add(Users users);

        void Update(Users users);

        void Delete(Users users);

        Task<Users> GetAsync(int id);
    }
}
