using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using MiniApi.Application;
using MiniApi.Application.Results.Users;
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
        public async Task<ActionResult> Register([FromBody] UsersModel model)
        {

            var result = await _usersQueries.RegisterUser(model);
            if (result)
            {
                return Ok("注册成功");
            }
            return BadRequest("注册失败");


        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="model">登录信息</param>
        /// <returns></returns>
        [HttpGet("Login")]
        [ProducesResponseType(typeof(AccessTokenResult), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> Login([FromQuery] UsersLoginModel model)
        {

            var data = await _usersQueries.Login(model);
            return Ok(data);


        }

        /// <summary>
        /// 获取用户详情
        /// </summary>
        /// <returns></returns>
        [HttpGet("UserInfo")]
        [ProducesResponseType(typeof(UsersInfoResult), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetUserDetails()
        {

            var data = await _usersQueries.GetUserDetails();
            if (data != null)
            {
                return Ok(data);
            }
            return NotFound("用户不存在");

        }

    }
}
