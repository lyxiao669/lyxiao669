using Domain.Aggregates;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminApi.Application.Queries
{
    public class UsersQueries:BaseQueries
    {
        readonly ApplicationDbContext _context;

        public UsersQueries(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PageResult<Users>> GetUsersListAsync(PageModel model)
        {
            var query = _context.Users;
            var list = await query
                //.OrderByDescending(a => a.Sort)
                .Page(model.PageIndex, model.PageSize)
                .ToListAsync();
            var count = await query.CountAsync();

            return PageResult(list, count);
          
        }
    }

}
