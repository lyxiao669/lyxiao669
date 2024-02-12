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
    /// 问题
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly QuestionQueries _queries;

        private readonly IMediator _mediator;

        public QuestionsController(QuestionQueries queries, IMediator mediator)
        {
            _queries = queries;
            _mediator = mediator;
        }

        /// <summary>
        /// 问题详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(Question), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> Get(int id)
        {
            var data = await _queries.GetQuestionDetailAsync(id);
            return Ok(data);
        }

        /// <summary>
        /// 问题列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(PageResult<Question>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> Get([FromQuery] QuestionModel model)
        {
            var data = await _queries.GetQuestionListAsync(model);
            return Ok(data);
        }

        /// <summary>
        /// 添加问题
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Post(CreateQuestionCommand command)
        {
            var result = await _mediator.Send(command);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }

        /// <summary>
        /// 修改问题
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Update(UpdateQuestionCommand command)
        {
            var result = await _mediator.Send(command);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }

        /// <summary>
        /// 删除问题
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteQuestionCommand()
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
