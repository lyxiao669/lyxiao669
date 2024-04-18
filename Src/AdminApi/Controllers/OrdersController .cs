using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using AdminApi.Application.Commands;
using AdminApi.Application.Queries;
using Domain.Aggregates;
using AdminApi.Application;

namespace AdminApi.Controllers
{
    /// <summary>
    /// 订单控制器
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly OrderQueries _orderQueries;
        private readonly IMediator _mediator;

        public OrdersController(OrderQueries orderQueries, IMediator mediator)
        {
            _orderQueries = orderQueries;
            _mediator = mediator;
        }

        /// <summary>
        /// 更新订单状态
        /// </summary>
        /// <param name="command">更新订单状态命令</param>
        /// <returns></returns>
        [HttpPut("UpdateStatus")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> UpdateStatus(UpdateOrderStatusCommand command)
        {
            var result = await _mediator.Send(command);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }

        /// <summary>
        /// 查询订单列表
        /// </summary>
        /// <param name="model">分页模型</param>
        /// <returns>订单列表</returns>
        [HttpGet]
        [ProducesResponseType(typeof(PageResult<Order>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> Get([FromQuery] OrdersModel  model)
        {
            var data = await _orderQueries.GetOrdersListAsync(model);
            return Ok(data);
        }

        /// <summary>
        /// 删除订单
        /// </summary>
        /// <param name="id">订单ID</param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteOrderCommand { Id = id };
            var result = await _mediator.Send(command);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
