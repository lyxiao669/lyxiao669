using Applet.API.Infrastructure;
using Domain.Aggregates;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MiniApi.Application
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, CreateOrderResult>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly UsersAccessor _usersAccessor;

        public CreateOrderCommandHandler(IOrderRepository orderRepository, UsersAccessor usersAccessor)
        {
            _orderRepository = orderRepository;
            _usersAccessor = usersAccessor;
        }

        public async Task<CreateOrderResult> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var userId = _usersAccessor.Id; // 获取当前用户

            // 创建订单实体
            var order = new Order
            {
                UserId = userId,
                SpotId = request.SpotId,
                // Status = request.Status,
                Status = 0,
                OrderDate = DateTime.Now
            };

            // 添加订单到仓库
            _orderRepository.Add(order);
            await _orderRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return new CreateOrderResult
            {
                OrderId = order.Id,
                Message = "订单创建成功"
            };
        }
    }
}
