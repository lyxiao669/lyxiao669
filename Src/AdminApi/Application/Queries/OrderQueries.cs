using Domain.Aggregates;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminApi.Application.Queries
{
    public class OrderQueries : BaseQueries
    {
        private readonly ApplicationDbContext _context;

        public OrderQueries(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 查询订单列表，包含用户和景点信息
        /// </summary>
        /// <param name="model">分页模型</param>
        /// <returns>包含用户和景点信息的订单列表</returns>
        public async Task<PageResult<OrderDetailResult>> GetOrdersListAsync(PageModel model)
        {
            var query = from order in _context.Order
                        join user in _context.Users on order.UserId equals user.Id
                        join spot in _context.ScenicSpots on order.SpotId equals spot.Id
                        select new OrderDetailResult
                        {
                            Id = order.Id,
                            OrderDate = order.OrderDate,
                            Status = order.Status,
                            UserId = user.Id,
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

            var list = await query
                .Page(model.PageIndex, model.PageSize)
                .ToListAsync();

            var count = await query.CountAsync();

            return PageResult(list, count);
        }
    }

    /// <summary>
    /// 订单详情结果模型
    /// </summary>
    // public class OrderDetailResult
    // {
    //     public int OrderId { get; set; }
    //     public DateTime OrderDate { get; set; }
    //     public int Status { get; set; }
    //     public int UserId { get; set; }
    //     public string UserName { get; set; }
    //     public string Avatar { get; set; }
    //     public int SpotId { get; set; }
    //     public string SpotName { get; set; }
    //     public string ProvinceName { get; set; }
    //     public string CityName { get; set; }
    //     public int Likes { get; set; }
    //     public string Description { get; set; }
    //     public decimal TicketPrice { get; set; }
    //     public double Latitude { get; set; }
    //     public double Longitude { get; set; }
    //     public string Images { get; set; }
    //     public string Address { get; set; }
    //     public string Telephone { get; set; }
    //     public string OpeningHours { get; set; }
    // }
}
