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
}
