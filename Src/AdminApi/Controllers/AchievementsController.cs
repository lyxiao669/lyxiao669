using AdminApi.Application;
using Domain.Aggregates;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace AdminApi.Controllers
{
    /// <summary>
    /// 用户成绩记录
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class AchievementsController : ControllerBase
    {
        readonly AchievementQueries _queries;

        public AchievementsController(AchievementQueries queries)
        {
            _queries = queries;
        }

        /// <summary>
        /// 成绩记录列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>

        [HttpGet]
        [ProducesResponseType(typeof(PageResult<Achievement>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> Get([FromQuery] AchievementModel model)
        {
            var data = await _queries.GetAchievementListAsync(model);
            return Ok(data);
        }
    }
}
