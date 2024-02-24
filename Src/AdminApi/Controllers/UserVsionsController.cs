using AdminApi.Application;
using AdminApi.Application.Queries;
using Domain.Aggregates;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace AdminApi.Controllers
{
    /// <summary>
    /// 学生视力信息
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class UserVsionsController : ControllerBase
    {
        readonly UserVsionQueries _queries;
        readonly IMediator _mediator;

        public UserVsionsController(UserVsionQueries queries, IMediator mediator)
        {
            _queries = queries;
            _mediator = mediator;
        }


        /// <summary>
        /// 学生视力信息列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(PageResult<UserVsion>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> Get([FromQuery] UserVsionModel model)
        {
            var data = await _queries.GetUserVsionListAsync(model);
            return Ok(data);
        }


        /// <summary>
        /// excel导入学生视力信息
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("Excel")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Post(ExcelImportUserVsionCommand command)
        {
            var result = await _mediator.Send(command);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }
        /// <summary>
        /// 添加学生视力信息
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Post(CreateUserVsionCommand command)
        {
            var result = await _mediator.Send(command);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }
        /// <summary>
        /// 修改学生视力信息
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Put(UpdateUserVsionCommand command)
        {
            var result = await _mediator.Send(command);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }
        /// <summary>
        /// 删除学生视力信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteUserVsionCommand()
            {
                Id = id
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
