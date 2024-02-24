using MiniApi.Application;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace MiniApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PhotosController : ControllerBase
    {
        readonly IMediator _mediator;
        readonly PhotoQueries _queries;

        public PhotosController(IMediator mediator, PhotoQueries queries)
        {
            _mediator = mediator;
            _queries = queries;
        }


        /// <summary>
        /// 照片墙
        /// </summary>
        /// <param name="number">照片墙上的照片数量</param>
        /// <returns></returns>
        [HttpGet("{number:int}")]
        [ProducesResponseType(typeof(List<string>), (int)HttpStatusCode.OK)]
        public ActionResult Get(int number)
        {
            var data =  _queries.GetPhotoListResultAsync(number);
            return Ok(data);
        }

        ///// <summary>
        ///// 最新拍照图片
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet("PhotoGraph")]
        //[ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        //public ActionResult GetPhotoGraph()
        //{
        //    var data =  _queries.GetPhotoGraphAsync();
        //    return Ok(data);
        //}

        /// <summary>
        /// 用户查看自己眼睛照片
        /// </summary>
        /// <returns></returns>
        [HttpGet("UserPhoto")]
        [ProducesResponseType(typeof(PhotoResult), (int)HttpStatusCode.OK)]
        public async Task<ActionResult> GetUserPhoto([FromQuery]string qrCode)
        {
            var data = await _queries.GetPhotoResultAsync(qrCode);
            return Ok(data);
        }


        /// <summary>
        /// 猫眼拍照
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("Photo")]
        [Authorize]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> Post(PhotoCommand command)
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
