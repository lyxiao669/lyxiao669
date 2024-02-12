using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Juzhen.Infrastructur;
using Juzhen.Qiniu.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Qiniu.Http;
using Qiniu.Storage;
using Qiniu.Util;
using static Juzhen.Infrastructur.ImgUtil;

namespace AdminApi.Controllers
{

    /// <summary>
    /// 上传统一接口
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {

        /// <summary>
        /// 七牛云
        /// </summary>
        /// <returns></returns>
        [HttpPost("CreateUploadToken")]
        public IActionResult GetQiniuUploadToken()
        {
            var data = QiniuUtil.GetUploadToken();
            var obj = new
            {
                accessToken = data,
                expireIn = 3600
            };
            return Ok(obj);
        }

        #region 上传本地统一接口
        /// <summary>
        /// 本地
        /// </summary>
        /// <param name="req"></param>
        [HttpPost]
        public void Upload([FromForm] UploadModel req)
        {
            var util = UploadLocalImg(req);
        }
        #endregion


    }



}