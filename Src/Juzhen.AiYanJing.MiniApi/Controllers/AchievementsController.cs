using Juzhen.AiYanJing.MiniApi.Application.Queries;
using Juzhen.Domain.Aggregates;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace Juzhen.AiYanJing.MiniApi.Controllers
{
    /// <summary>
    /// pc端答题显示分数
    /// </summary>
    [Route("[controller]")]
    [Authorize]
    [ApiController]
    public class AchievementsController : ControllerBase
    {
        readonly AchievementQueries _queries;

        public AchievementsController(AchievementQueries queries)
        {
            _queries = queries;
        }

        /// <summary>
        /// 用户答题结果
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(Achievement), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> Get()
        {
            var data = await _queries.GetAchievementAsync();
            return Ok(data);
        }


    }
}
