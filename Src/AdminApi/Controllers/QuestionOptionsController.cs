using AdminApi.Application;
using Domain.Aggregates;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace AdminApi.Controllers
{
    /// <summary>
    /// 问题选项
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class QuestionOptionsController : ControllerBase
    {
        readonly QuestionOptionQueries _queries;
        readonly IMediator _mediator;

        public QuestionOptionsController(QuestionOptionQueries queries, IMediator mediator)
        {
            _queries = queries;
            _mediator = mediator;
        }
        /// <summary>
        /// 问题选项详情
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(QuestionOptions), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> Get(int id)
        {
            var data = await _queries.GetQuestionOptionDetailAsync(id);
            return Ok(data);
        }


        /// <summary>
        /// 问题选项列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(PageResult<QuestionOptions>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> Get([FromQuery] QuestionOptionModel model)
        {
            var data = await _queries.GetQuestionOptionListAsync(model);
            return Ok(data);
        }

        /// <summary>
        /// 添加问题选项
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Post(CreateQuestionOptionsCommand command)
        {
            var result = await _mediator.Send(command);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }

        /// <summary>
        /// 修改问题选项
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Update(UpdateQuestionOptionsCommand command)
        {
            var result = await _mediator.Send(command);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }

        /// <summary>
        /// 删除问题选项
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteQuestionOptionsCommand()
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
