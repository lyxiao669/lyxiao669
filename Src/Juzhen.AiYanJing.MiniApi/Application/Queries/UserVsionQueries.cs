using Applet.API.Infrastructure;
using Juzhen.Domain.Aggregates;
using Juzhen.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Juzhen.AiYanJing.MiniApi.Application
{
    public class UserVsionQueries:BaseQueries
    {
        readonly UserAccessor _userAccessor;
        readonly ApplicationDbContext _context;

        public UserVsionQueries(ApplicationDbContext context, UserAccessor userAccessor)
        {
            _context = context;
            _userAccessor = userAccessor;
        }

        /// <summary>
        /// 用户视力结果
        /// </summary>
        /// <returns></returns>
        public async Task<UserVsion> GetUserVsionAsync()
        {
            var result = await _context.UserVsions
                .Where(a => a.FullName == _userAccessor.FullName && a.Mobile == _userAccessor.Mobile)
                .OrderByDescending(a=>a.CreateTime)
                .FirstOrDefaultAsync();

            return result;
        }
    }
}
