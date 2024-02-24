using Juzhen.MiniProgramAPI.Infrastructure.Utils;
using Juzhen.Qiniu.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Juzhen.RoadProgramApp.Controllers
{
    /// <summary>
    /// 七牛云
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        /// <summary>
        /// 七牛云token
        /// </summary>
        /// <returns></returns>
        [HttpPost("CreateUploadToken")]
        public IActionResult GetQiniuUploadToken()
        {
            var data = QiniuUtil.GetUploadToken();
            return Ok(data);
        }
    }
}
