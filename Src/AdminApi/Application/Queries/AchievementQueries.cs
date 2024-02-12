using Juzhen.Domain.Aggregates;
using Juzhen.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AdminApi.Application
{
    public class AchievementQueries:BaseQueries
    {
        readonly ApplicationDbContext _context;

        public AchievementQueries(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 成绩列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>

        public async Task<PageResult<Achievement>> GetAchievementListAsync(AchievementModel model)
        {
            var query = _context.Achievements
                .WhereIF(a=>a.UserId==model.UserId,model.UserId!=null);

            var list = await query
                .OrderByDescending(a => a.CreateTime)
                .Page(model.PageIndex, model.PageSize)
                .ToListAsync();

            var count = await query.CountAsync();

            return PageResult(list, count);
        }
    }
}
