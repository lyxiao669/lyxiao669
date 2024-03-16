using Applet.API.Infrastructure;
using Domain.Aggregates;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniApi.Application.Queries
{
    public class UserFavoriteQueries : BaseQueries
    {
        private readonly ApplicationDbContext _context;
        private readonly UsersAccessor _usersAccessor;

        public UserFavoriteQueries(ApplicationDbContext context, UsersAccessor usersAccessor)
        {
            _context = context;
            _usersAccessor = usersAccessor;
        }

        /// <summary>
        /// 根据用户查询收藏列表
        /// </summary>
        /// <returns>收藏列表</returns>
        public async Task<List<FavoriteDetailResult>> GetFavoritesByUserIdAsync()
        {
            var userId = _usersAccessor.Id;
            var user = await _context.Users.FindAsync(userId);

            if (user == null)
            {
                // 如果找不到用户，返回空列表
                return new List<FavoriteDetailResult>();
            }

            var favorites = await _context.UserFavorites
                .Where(f => f.UserId == userId)
                .Include(f => f.ScenicSpot)
                .Select(f => new FavoriteDetailResult
                {
                    FavoriteId = f.Id,
                    UserName = user.UserName,
                    Avatar = user.Avatar,
                    SpotName = f.ScenicSpot.SpotName,
                    ProvinceName = f.ScenicSpot.ProvinceName,
                    CityName = f.ScenicSpot.CityName,
                    Likes = f.ScenicSpot.Likes,
                    Description = f.ScenicSpot.Description,
                    TicketPrice = f.ScenicSpot.TicketPrice,
                    Latitude = f.ScenicSpot.Latitude,
                    Longitude = f.ScenicSpot.Longitude,
                    Images = f.ScenicSpot.Images,
                    Address = f.ScenicSpot.Address,
                    Telephone = f.ScenicSpot.Telephone,
                    OpeningHours = f.ScenicSpot.OpeningHours,
                })
                .ToListAsync();

            return favorites;
        }
    }
}
