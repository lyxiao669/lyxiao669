using Juzhen.Domain.Aggregates;
using Juzhen.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminApi.Application.Queries
{
    public class ScenicSpotQueries:BaseQueries
    {
        readonly ApplicationDbContext _context;

        public ScenicSpotQueries(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PageResult<ScenicSpots>> GetScenicSpotsListAsync(PageModel model)
        {
            var query = _context.ScenicSpots;
            var list = await query
                //.OrderByDescending(a => a.Sort)
                .Page(model.PageIndex, model.PageSize)
                .ToListAsync();
            var count = await query.CountAsync();

            return PageResult(list, count);
          
        }
    }

}
