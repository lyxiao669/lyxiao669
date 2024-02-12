using Applet.API.Infrastructure;
using Juzhen.Domain.Aggregates;
using Juzhen.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Juzhen.AiYanJing.MiniApi.Application.Queries
{
    public class AchievementQueries:BaseQueries
    {
        readonly UserAccessor _userAccessor;
        readonly ApplicationDbContext _context;

        public AchievementQueries(ApplicationDbContext context, UserAccessor userAccessor)
        {
            _context = context;
            _userAccessor = userAccessor;
        }

        /// <summary>
        /// 用户成绩
        /// </summary>
        /// <returns></returns>
        public async Task<Achievement> GetAchievementAsync()
        {
            var result = await _context.Achievements
                .Where(a => a.UserId == _userAccessor.Id)
                .OrderByDescending(a => a.CreateTime)
                .FirstOrDefaultAsync();

            return result;
        }
    }
}
