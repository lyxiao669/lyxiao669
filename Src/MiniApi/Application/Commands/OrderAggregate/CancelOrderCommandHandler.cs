using Applet.API.Infrastructure;
using Domain.Aggregates;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MiniApi.Application
{
    public class CancelOrderCommandHandler : IRequestHandler<CancelOrderCommand, CanceleOrderResult>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly UsersAccessor _usersAccessor;

        public CancelOrderCommandHandler(IOrderRepository orderRepository, UsersAccessor usersAccessor)
        {
            _orderRepository = orderRepository;
            _usersAccessor = usersAccessor;
        }

        public async Task<CanceleOrderResult> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
        {
            var userId = _usersAccessor.Id; // 获取当前用户ID

            // 获取订单
            var order = await _orderRepository.GetAsync(request.OrderId);

            // 检查订单是否存在以及当前用户是否有权限取消该订单
            if (order == null || order.UserId != userId)
            {
                return new CanceleOrderResult
                {
                    Success = false,
                    Message = "订单不存在或无权取消",
                };
            }

            // 更新订单状态为已取消
            order.Status = 2;

            // 更新订单
            _orderRepository.Update(order);
            await _orderRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return new CanceleOrderResult
            {
                Success = true,
                Message = "订单已成功取消",
            };
        }
    }
}
