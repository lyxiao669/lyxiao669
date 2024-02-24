using Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Aggregates
{
    public interface IIdentityUserRepository:IRepository<IdentityUser>
    {
        void Add(IdentityUser user);

        void Update(IdentityUser user);

        void Delete(IdentityUser user);

        Task<IdentityUser> GetAsync(int id);

        Task<List<IdentityUser>> GetNotQrCodeImgList();
    }
}
