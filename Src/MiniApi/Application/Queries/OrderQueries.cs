using Applet.API.Infrastructure;
using Domain.Aggregates;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using MiniApi.Application.Queries;
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

      if (user == null)
      {
        // 如果找不到用户，返回空列表
        return new List<OrderDetailWithSpotResult>();
      }

      var orders = await _context.Order
          .Where(o => o.UserId == userId)
          .ToListAsync();

      var orderDetailsList = new List<OrderDetailWithSpotResult>();

      foreach (var order in orders)
      {
        var spot = await _context.ScenicSpots.FindAsync(order.SpotId);
        if (spot != null)
        {
          var orderDetail = new OrderDetailWithSpotResult
          {
            OrderId = order.Id,
            OrderDate = order.OrderDate,
            Status = order.Status,
            UserId = userId,
            UserName = user.UserName,
            Avatar = user.Avatar,
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
          orderDetailsList.Add(orderDetail);
        }
      }

      return orderDetailsList;
    }


  }
}
