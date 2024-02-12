using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace Juzhen.AiYanJing.CompositePictureApi.Controllers
{
    /// <summary>
    /// 合成二维码图片
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class QrCodesController : ControllerBase
    {
        readonly IMediator _mediator;

        public QrCodesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// 合成二维码图片
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Post(CreateQrCodeImgCommand command)
        {
            var result = await _mediator.Send(command);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }

        /// <summary>
        /// 合成猫眼合照图片
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("photo")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Post(CreateCompositePhotoCommand command)
        {
            var result = await _mediator.Send(command);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }



    }
}