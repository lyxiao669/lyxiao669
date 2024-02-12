using Juzhen.Domain.Aggregates;
using Juzhen.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AdminApi.Application.Queries
{
    public class UserVsionQueries:BaseQueries
    {
        readonly ApplicationDbContext _context;

        public UserVsionQueries(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 用户视力列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<PageResult<UserVsion>> GetUserVsionListAsync(UserVsionModel model)
        {
            var query = _context.UserVsions
                .WhereIF(a => a.FullName.Contains(model.FullName), model.FullName != null)
                .WhereIF(a => a.Mobile.Contains(model.Mobile), model.Mobile != null);

            var list = await query
                .OrderByDescending(a => a.Id)
                .Page(model.PageIndex, model.PageSize)
                .ToListAsync();

            var count = await query.CountAsync();

            return PageResult(list, count);
        }
    }
}
