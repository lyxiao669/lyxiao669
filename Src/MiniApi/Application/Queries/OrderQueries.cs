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
    /// 根据用户查询订单，返回结果按订单时间降序排列
    /// </summary>
    /// <param name="status">订单状态，当为null、空或-1时返回所有状态的订单</param>
    /// <returns>订单列表</returns>
    public async Task<List<OrderDetailWithSpotResult>> GetOrderDetailsByUserIdAsync(int? status)
    {
      var userId = _usersAccessor.Id;
      var user = await _context.Users.FindAsync(userId);

      if (user == null)
      {
        // 如果找不到用户，返回空列表
        return new List<OrderDetailWithSpotResult>();
      }

      // 根据状态过滤订单，如果状态为null、空或-1，则返回所有订单
      IQueryable<Order> ordersQuery = _context.Order
                                            .Where(o => o.UserId == userId);
      if (status.HasValue && status.Value >= 0)
      {
        ordersQuery = ordersQuery.Where(o => o.Status == status.Value);
      }

      // 根据时间降序排序
      ordersQuery = ordersQuery.OrderByDescending(o => o.OrderDate);

      var orders = await ordersQuery.ToListAsync();

      var orderDetailsList = new List<OrderDetailWithSpotResult>();
      foreach (var order in orders)
      {
        var spot = await _context.ScenicSpots.FindAsync(order.SpotId);
        if (spot != null)
        {
          var orderDetail = new OrderDetailWithSpotResult
          {
            SpotId = order.SpotId,
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
