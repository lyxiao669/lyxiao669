using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using MiniApi.Application;
using Juzhen.MiniProgramAPI; // 确保这里的命名空间正确指向您的UsersQueries所在的命名空间

namespace MiniApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly UsersQueries _usersQueries;

        public UsersController(IMediator mediator, UsersQueries usersQueries)
        {
            _mediator = mediator;
            _usersQueries = usersQueries;
        }

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="model">注册信息</param>
        /// <returns></returns>
        [HttpPost("Register")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Register([FromBody] UsersModel model)
        {
            try
            {
                var result = await _usersQueries.RegisterUser(model);
                if (result)
                {
                    return Ok("注册成功");
                }
                return BadRequest("注册失败");
            }
            catch (ServiceException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="model">登录信息</param>
        /// <returns></returns>
        [HttpPost("Login")]
        [ProducesResponseType(typeof(AccessTokenResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Login([FromBody] UsersModel model)
        {
            try
            {
                var data = await _usersQueries.Login(model);
                return Ok(data);
            }
            catch (ServiceException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// 获取用户详情
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns></returns>
        [HttpGet("{userId}")]
        [ProducesResponseType(typeof(UsersInfoResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> GetUserDetails([FromRoute] int userId)
        {
            try
            {
                var data = await _usersQueries.GetUserDetails(userId);
                if (data != null)
                {
                    return Ok(data);
                }
                return NotFound("用户不存在");
            }
            catch (ServiceException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
