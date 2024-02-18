using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using AdminApi.Application.Commands.ScenicSpotsAggregate;
using AdminApi.Application;
using Juzhen.Domain.Aggregates;
using AdminApi.Application.Queries; 

namespace AdminApi.Controllers
{
    [Route("api/[controller]")]
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
        /// 创建景区
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Post(CreateScenicSpotCommand command)
        {
            var result = await _mediator.Send(command);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Update(UpdateScenicSpotCommand command)
        {
            var result = await _mediator.Send(command);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet]
        [ProducesResponseType(typeof(PageResult<ScenicSpots>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> Get([FromQuery] PageModel model)
        {
            var data = await _scenicSpotsQueries.GetScenicSpotsListAsync(model);
            return Ok(data);
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Delete(int id)
        {
            var command = new DeleteScenicSpotCommand { Id = id };
            var result = await _mediator.Send(command);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
