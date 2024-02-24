using MiniApi.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace MiniApi.Controllers
{
    /// <summary>
    /// 用户视力
    /// </summary>
    [Route("[controller]")]
    [Authorize]
    [ApiController]
    public class UserVsionsController : ControllerBase
    {
        readonly UserVsionQueries _queries;

        public UserVsionsController(UserVsionQueries queries)
        {
            _queries = queries;
        }


        /// <summary>
        /// 用户查看自己的视力结果数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(UserinfoResult), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> Get()
        {
            var data = await _queries.GetUserVsionAsync();
            return Ok(data);
        }
    }
}
