using AdminApi.Application;
using Juzhen.Domain.Aggregates;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace AdminApi.Controllers
{
    /// <summary>
    /// 题库配置
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class QuestionBanksController : ControllerBase
    {
        readonly IMediator _mediator;

        readonly QuestionBankQueries _queries;

        public QuestionBanksController(IMediator mediator, QuestionBankQueries queries)
        {
            _mediator = mediator;
            _queries = queries;
        }

        /// <summary>
        /// 题库配置详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(QuestionBank), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> Get(int id)
        {
            var data = await _queries.GetQuestionBankDetailAsync(id);
            return Ok(data);
        }

        /// <summary>
        /// 题库配置列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(PageResult<QuestionBank>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> Get([FromQuery] AchievementModel model)
        {
            var data = await _queries.GetQuestionBankListAsync(model);
            return Ok(data);
        }


        /// <summary>
        /// 添加题库配置
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Post(CreateQuestionBankCommand command)
        {
            var result = await _mediator.Send(command);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }

        /// <summary>
        /// 修改题库配置
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Update(UpdateQuestionBankCommand command)
        {
            var result = await _mediator.Send(command);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }

        /// <summary>
        /// 删除题库配置
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteQuestionBankCommand()
            {
                Id = id,
            };
            var result = await _mediator.Send(command);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
