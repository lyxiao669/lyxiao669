using Applet.API.Infrastructure;
using Domain.Aggregates;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniApi.Application
{
  public class OrderQueries : BaseQueries
  {
    private readonly ApplicationDbContext _context;
    readonly UsersAccessor _usersAccessor;

    public OrderQueries(ApplicationDbContext context, UsersAccessor usersAccessor)
    {
      _context = context;
      _usersAccessor = usersAccessor;
    }

    /// <summary>
    /// 根据用户查询订单
    /// </summary>
    /// <returns>订单列表</returns>
    public async Task<List<OrderDetailWithSpotResult>> GetOrderDetailsByUserIdAsync()
    {
      var userId = _usersAccessor.Id;
      var user = await _context.Users.FindAsync(userId);

      // 如果找不到用户，则直接返回空列表
      if (user == null)
      {
        return new List<OrderDetailWithSpotResult>();
      }

      var orderDetails = await _context.Order
          .Where(o => o.UserId == userId)
          .Join(_context.ScenicSpots,
                order => order.SpotId,
                spot => spot.Id,
                (order, spot) => new { order, spot })
          .Select(joined => new OrderDetailWithSpotResult
          {
            OrderId = joined.order.Id,
            OrderDate = joined.order.OrderDate,
            Status = joined.order.Status,
            UserId = userId, // 包含用户ID
            UserName = user.UserName,
            Avatar = user.Avatar,
            SpotName = joined.spot.SpotName,
            ProvinceName = joined.spot.ProvinceName,
            CityName = joined.spot.CityName,
            Likes = joined.spot.Likes,
            Description = joined.spot.Description,
            TicketPrice = joined.spot.TicketPrice,
            Latitude = joined.spot.Latitude,
            Longitude = joined.spot.Longitude,
            Images = joined.spot.Images,
            Address = joined.spot.Address,
            Telephone = joined.spot.Telephone,
            OpeningHours = joined.spot.OpeningHours
          })
          .ToListAsync();

      return orderDetails;
    }



  }
}
