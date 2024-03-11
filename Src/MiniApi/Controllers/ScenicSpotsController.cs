using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using Domain.Aggregates;
using MiniApi.Application.Queries;
using MiniApi.Application;

namespace AdminApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ScenicSpotsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ScenicSpotQueries _scenicSpotsQueries;

        public ScenicSpotsController(ScenicSpotQueries scenicSpotsQueries, IMediator mediator)
        {
            _scenicSpotsQueries = scenicSpotsQueries;
            _mediator = mediator;
        }

        /// <summary>
        /// 获取景区列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(PageResult<ScenicSpots>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> Get([FromQuery] ScenicSpotModel model)
        {
            var data = await _scenicSpotsQueries.GetScenicSpotsListAsync(model);
            return Ok(data);
        }

        /// <summary>
        /// 根据id获取景区
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("ScenicSpotDetail")]
        [ProducesResponseType(typeof(PageResult<ScenicSpots>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetScenicSpotsById(int id)
        {
            var data = await _scenicSpotsQueries.GetScenicSpotsListById(id);
            return Ok(data);
        }
    }
}
