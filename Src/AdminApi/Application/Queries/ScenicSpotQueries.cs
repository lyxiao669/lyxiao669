using Domain.Aggregates;
using Infrastructure;
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

        public async Task<PageResult<ScenicSpots>> GetScenicSpotsListAsync(ScenicSpotModel model)
        {
            var query = _context.ScenicSpots
            .Where(s => model.KeyWord == null || s.ProvinceName.Contains(model.KeyWord) || s.CityName.Contains(model.KeyWord));
            var list = await query
                .OrderByDescending(a => a.Id)
                .Page(model.PageIndex, model.PageSize)
                .ToListAsync();
            var count = await query.CountAsync();

            return PageResult(list, count);
          
        }
    }

}
