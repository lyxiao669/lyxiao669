using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using AdminApi.Application;
using Domain.Aggregates;
using AdminApi.Application.Queries; // 请替换为实际的用户聚合命名空间

namespace AdminApi.Controllers
{
    /// <summary>
    /// 用户
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UsersQueries _userQueries;
        private readonly IMediator _mediator;

        public UsersController(UsersQueries userQueries, IMediator mediator)
        {
            _userQueries = userQueries;
            _mediator = mediator;
        }

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Post(CreateUsersCommand command)
        {
            var result = await _mediator.Send(command);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }

        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Update(UpdateUsersCommand command)
        {
            var result = await _mediator.Send(command);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }

        /// <summary>
        /// 查询用户列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(PageResult<Users>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> Get([FromQuery] PageModel model)
        {
            var data = await _userQueries.GetUsersListAsync(model);
            return Ok(data);
        }

        // <summary>
        // 删除用户
        // </summary>
        // <param name="id"></param>
        // <returns></returns>
        [HttpDelete("{id:int}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteUsersCommand()
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
