using AdminApi.Application;
using AdminApi.Application.Queries;
using Juzhen.Domain.Aggregates;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace AdminApi.Controllers
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
        /// 新增轮播图
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Post(CreateBannerCommand command)
        {
            var result = await _mediator.Send(command);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }

        /// <summary>
        /// 修改轮播图
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Update(UpdateBannerCommand command)
        {
            var result = await _mediator.Send(command);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }

        /// <summary>
        /// 查询轮播图列表
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


        /// <summary>
        /// 删除轮播图
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteBannerCommand()
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

