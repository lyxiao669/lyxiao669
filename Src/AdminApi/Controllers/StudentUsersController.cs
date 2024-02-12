using AdminApi.Application;
using Juzhen.Domain.Aggregates;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace AdminApi.Controllers
{
    /// <summary>
    /// 学生用户模块
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class StudentUsersController : ControllerBase
    {
        readonly StudentUserQueries _queries;
        readonly IMediator _mediator;

        public StudentUsersController(StudentUserQueries queries, IMediator mediator)
        {
            _queries = queries;
            _mediator = mediator;
        }

        /// <summary>
        /// 学生详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(IdentityUser), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> Get(int id)
        {
            var data = await _queries.GetIdentityUserDetailAsync(id);
            return Ok(data);
        }

        /// <summary>
        /// 学生年级列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("grade")]
        [ProducesResponseType(typeof(List<string>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> Get()
        {
            var data = await _queries.GetStudentGradeListAsync();
            return Ok(data);
        }

        /// <summary>
        /// 学生列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(PageResult<IdentityUser>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> Get([FromQuery] IdentityUserModel model)
        {
            var data = await _queries.GetIdentityUserListAsync(model);
            return Ok(data);
        }


        /// <summary>
        /// excel导入学生
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("Excel")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Post(ExcelImportIdentityUserCommand command)
        {
            var result = await _mediator.Send(command);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }
        /// <summary>
        /// 添加学生
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Post(CreateIdentityUserCommand command)
        {
            var result = await _mediator.Send(command);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }
        /// <summary>
        /// 修改学生
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Put(UpdateIdentityUserCommand command)
        {
            var result = await _mediator.Send(command);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }
        /// <summary>
        /// 删除学生
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteIdentityUserCommand()
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
