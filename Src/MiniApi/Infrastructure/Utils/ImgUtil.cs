using Juzhen.MiniProgramAPI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QRCoder;
using System;
using System.Drawing;
using System.IO;
using System.Net;
namespace Juzhen.Infrastructur
{
    /// <summary>
    /// 合成图片工具
    /// </summary>
    public static class ImgUtil
    {
        static string path = "Upload/DefaultFile";
        /// <summary>
        /// 上传文件到本地
        /// </summary>
        /// <returns></returns>
        public static JsonResult UploadLocalImg(UploadModel model)
        {
            //文件所在服务器路径
            try
            {
                #region 保存路径，如果不存在则创建
                var savepath = string.Format("{0}/wwwroot/{1}", Environment.CurrentDirectory, path);
                if (!Directory.Exists(savepath))
                {
                    Directory.CreateDirectory(savepath);
                }
                #endregion

                #region 请求分发
                var uri = UpoladImage(model);

                return new JsonResult(new
                {
                    Result = true,
                    Message = "success",
                    Data = uri
                });
                #endregion
            }
            catch (Exception e)
            {
                return new JsonResult(new
                {
                    Result = false,
                    e.Message
                });
            }
        }


        #region 上传文件到本地
        /// <summary>
        /// 上传图片
        /// </summary>
        private static string UpoladImage(UploadModel req)
        {
            var savepath = string.Format("{0}/wwwroot/{1}", Environment.CurrentDirectory, path);
            //文件信息
            var file = req.File;
            var filename = Guid.NewGuid().ToString("N");
            var extension = Path.GetExtension(file.FileName);
            var imagepath = string.Format("{0}/{1}{2}", savepath, filename, extension);
            using (var stream = file.OpenReadStream())
            {
                using (var fs = new FileStream(imagepath, FileMode.CreateNew))
                {
                    stream.CopyTo(fs);
                }
            }
            return string.Format("{0}{1}{2}", string.Format("/{0}/", path, filename, extension));
        }

        /// <summary>
        /// 上传文件请求参数
        /// </summary>
        public class UploadModel
        {
            /// <summary>
            /// 文件
            /// </summary>
            public IFormFile File { get; set; }
        }
        #endregion
    }
}
