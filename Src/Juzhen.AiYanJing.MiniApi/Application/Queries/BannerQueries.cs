using Juzhen.Domain.Aggregates;
using Juzhen.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Juzhen.AiYanJing.MiniApi.Application
{
    public class BannerQueries:BaseQueries
    {
        readonly ApplicationDbContext _context;

        public BannerQueries(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PageResult<Banner>> GetBannerListAsync(PageModel model)
        {
            var query = _context.Banners;
            var list = await query
                .OrderByDescending(a => a.Sort)
                .Page(model.PageIndex, model.PageSize)
                .ToListAsync();
            var count = await query.CountAsync();

            return PageResult(list, count);
          
        }
    }

}
