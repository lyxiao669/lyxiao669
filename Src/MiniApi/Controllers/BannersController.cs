
using MiniApi.Application;
using Domain.Aggregates;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace MiniApi.Controllers
{

    /// <summary>
    /// 轮播图
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class BannersController : ControllerBase
    {
        private readonly BannerQueries _bannerQueries;
        private readonly IMediator _mediator;

        public BannersController(BannerQueries bannerQueries, IMediator mediator)
        {
            _bannerQueries = bannerQueries;
            _mediator = mediator;
        }

      
        /// <summary>
        /// 轮播图列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(PageResult<Banner>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> Get([FromQuery] PageModel model)
        {
            var data = await _bannerQueries.GetBannerListAsync(model);
            return Ok(data);
        }


    }
    
}

