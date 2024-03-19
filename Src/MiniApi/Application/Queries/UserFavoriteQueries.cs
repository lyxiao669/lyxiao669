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
        /// 根据用户查询收藏列表，包括景区的详细信息
        /// </summary>
        /// <returns>收藏列表，包含景区的详细信息</returns>
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
                .ToListAsync();

            var favoriteDetailsList = new List<FavoriteDetailResult>();

            foreach (var favorite in favorites)
            {
                var spot = await _context.ScenicSpots.FindAsync(favorite.SpotId);
                if (spot != null)
                {
                    var favoriteDetail = new FavoriteDetailResult
                    {
                        FavoriteId = favorite.Id,
                        UserId = userId,
                        UserName = user.UserName,
                        Avatar = user.Avatar,
                        SpotId = spot.Id,
                        SpotName = spot.SpotName,
                        ProvinceName = spot.ProvinceName,
                        CityName = spot.CityName,
                        Likes = spot.Likes,
                        Description = spot.Description,
                        TicketPrice = spot.TicketPrice,
                        Latitude = spot.Latitude,
                        Longitude = spot.Longitude,
                        Images = spot.Images,
                        Address = spot.Address,
                        Telephone = spot.Telephone,
                        OpeningHours = spot.OpeningHours
                    };
                    favoriteDetailsList.Add(favoriteDetail);
                }
            }

            return favoriteDetailsList;
        }

        public async Task<bool> IsFavoriteAsync( int spotId)
        {
            var userId = _usersAccessor.Id;
            return await _context.UserFavorites.AnyAsync(f => f.UserId == userId && f.SpotId == spotId);
        }

    }
}
