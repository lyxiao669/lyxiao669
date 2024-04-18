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
    /// 反馈控制器
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly FeedbackQueries _feedbackQueries;
        private readonly IMediator _mediator;

        public FeedbackController(FeedbackQueries feedbackQueries, IMediator mediator)
        {
            _feedbackQueries = feedbackQueries;
            _mediator = mediator;
        }

        /// <summary>
        /// 查询反馈列表
        /// </summary>
        /// <param name="model">分页模型</param>
        /// <returns>反馈列表</returns>
        [HttpGet]
        [ProducesResponseType(typeof(PageResult<FeedbackDetailResult>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> Get([FromQuery] FeedbackModel model)
        {
            var data = await _feedbackQueries.GetFeedbackListAsync(model);
            return Ok(data);
        }

        /// <summary>
        /// 删除反馈
        /// </summary>
        /// <param name="id">反馈ID</param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteFeedbackCommand { Id = id };
            var result = await _mediator.Send(command);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }

        /// <summary>
        /// 更新反馈状态
        /// </summary>
        /// <param name="command">更新状态的命令</param>
        /// <returns></returns>
        [HttpPut("UpdateStatus")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> UpdateStatus(UpdateFeedbackStatusCommand command)
        {
            var result = await _mediator.Send(command);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
