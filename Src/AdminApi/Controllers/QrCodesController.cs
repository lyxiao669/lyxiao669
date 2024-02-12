
using AdminApi.Application;

using Juzhen.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Juzhen.Admin.HttpApi.Controllers
{
    /// <summary>
    /// 合成二维码图片
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class QrCodesController : ControllerBase
    {
        readonly IMediator _mediator;
        readonly QrCodeQueries _queries;

        public QrCodesController(IMediator mediator, QrCodeQueries queries)
        {
            _mediator = mediator;
            _queries = queries;
        }


        /// <summary>
        /// 导出二维码图片压缩包
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public ActionResult Get([FromQuery]QrCodeModel model)
        {
            var data =  _queries.GetQrCodeZipAsync(model);

            return File(data, "application/vnd.ms-excel", "学生二维码压缩包.zip");
        }

        ///// <summary>
        ///// 合成二维码图片
        ///// </summary>
        ///// <param name="command"></param>
        ///// <returns></returns>
        //[HttpPost]
        //[ProducesResponseType((int)HttpStatusCode.OK)]
        //[ProducesResponseType((int)HttpStatusCode.BadRequest)]
        //public async Task<ActionResult> Post(CreateQrCodeImgCommand command)
        //{
        //    var result = await _mediator.Send(command);
        //    if (result)
        //    {
        //        return Ok();
        //    }
        //    return BadRequest();
        //}

        ///// <summary>
        ///// 合成合照图片
        ///// </summary>
        ///// <param name="command"></param>
        ///// <returns></returns>
        //[HttpPost("photo")]
        //[ProducesResponseType((int)HttpStatusCode.OK)]
        //[ProducesResponseType((int)HttpStatusCode.BadRequest)]
        //public async Task<ActionResult> Post(CreateCompositePhotoCommand command)
        //{
        //    var result = await _mediator.Send(command);
        //    if (result)
        //    {
        //        return Ok();
        //    }
        //    return BadRequest();
        //}



    }
}