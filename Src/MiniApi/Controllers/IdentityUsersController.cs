using MiniApi.Application;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace MiniApi.Controllers
{
    /// <summary>
    /// 用户模块
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class IdentityUsersController : ControllerBase
    {
        readonly IMediator _mediator;

        readonly IdentityUserQueries _queries;

        public IdentityUsersController(IMediator mediator, IdentityUserQueries queries)
        {
            _mediator = mediator;
            _queries = queries;
        }

        /// <summary>
        /// 小程序用户登录
        /// </summary>
        /// <returns></returns>
        [HttpGet("Login")]
        [ProducesResponseType(typeof(AccessTokenResult), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> Login([FromQuery]IdentiyUserModel model)
        {
            var data = await _queries.Login(model);
            return Ok(data);
        }

        /// <summary>
        /// 扫码登录（扫码获取token）
        /// </summary>
        /// <returns></returns>
        [HttpGet("qRCode")]
        [ProducesResponseType(typeof(AccessTokenResult), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> Get(string qRCode)
        {
            var data = await _queries.GetQrcodeUserInfo(qRCode);
            return Ok(data);
        }


        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(UserinfoResult), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> Get()
        {
            var data = await _queries.GetUserInfo();
            return Ok(data);
        }




    }
}
