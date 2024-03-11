using Domain.Aggregates;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniApi.Application.Queries
{
    public class ScenicSpotQueries : BaseQueries
    {
        readonly ApplicationDbContext _context;

        public ScenicSpotQueries(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PageResult<ScenicSpots>> GetScenicSpotsListAsync(ScenicSpotModel model)
        {
            var query = _context.ScenicSpots
            .Where(s => model.KeyWord == null || s.SpotName.Contains(model.KeyWord) || s.ProvinceName.Contains(model.KeyWord) || s.CityName.Contains(model.KeyWord))
            .Where(x => model.cityName == null || x.CityName.Contains(model.cityName));
            var list = await query
                .OrderByDescending(a => a.Likes)
                .Page(model.PageIndex, model.PageSize)
                .ToListAsync();
            var count = await query.CountAsync();

            return PageResult(list, count);

        }

        public async Task<ScenicSpots> GetScenicSpotsListById(int Id)
        {
            var scenicSpot = await _context.ScenicSpots
                .FirstOrDefaultAsync(a => a.Id == Id);

            return scenicSpot;

        }
    }

}
