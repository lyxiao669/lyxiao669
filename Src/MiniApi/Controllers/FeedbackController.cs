using MiniApi.Application;
using Domain.Aggregates;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MiniApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly FeedbackQueries _feedbackQueries;

        public FeedbackController(IMediator mediator, FeedbackQueries feedbackQueries)
        {
            _mediator = mediator;
            _feedbackQueries = feedbackQueries;
        }

        /// <summary>
        /// 创建反馈
        /// </summary>
        /// <param name="command">创建反馈的命令</param>
        /// <returns>创建结果</returns>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(typeof(CreateFeedbackResult), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> CreateFeedback(CreateFeedbackCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        /// <summary>
        /// 获取当前用户的反馈列表
        /// </summary>
        /// <returns>反馈详情列表</returns>
        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(List<FeedbackDetailResult>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<FeedbackDetailResult>>> GetFeedbackDetails()
        {
            var feedbackDetails = await _feedbackQueries.GetFeedbackDetailsByUserIdAsync();
            return Ok(feedbackDetails);
        }
    }
}
