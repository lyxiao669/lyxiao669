using Juzhen.Infrastructure;
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

        /// <summary>
        /// 生成二维码合成图片
        /// </summary>
        /// <param name="qrcode"></param>
        /// /// <param name="fullName"></param>
        /// <returns></returns>
        public static byte[] UploadQrCode(string qrcode, string fullName)
        {

                #region 请求分发
                var uri = CreateQR(qrcode, fullName);

                return uri;
                #endregion
        }


        /// <summary>
        /// 两张图片合成
        /// </summary>
        /// <param name="img">需要合成进去的图</param>
        /// <param name="backgroundImg">背景图</param>
        /// <param name="photo">照片</param>
        /// <returns></returns>
        public static byte[] UploadImg(string img, string backgroundImg, string photo)
        {
                #region 请求分发
                var uri = CreateImg(img, backgroundImg, photo);

                return uri;
                #endregion
        }


        #region 图片合成方法
        /// <summary>
        /// 合成二维码加文字图片
        /// </summary>
        /// <param name="nr">生成二维码字段</param>
        /// <param name="fullName">文字内容</param>
        /// <returns></returns>
        private static byte[] CreateQR(string nr, string fullName)
        {

            if (!string.IsNullOrEmpty(nr))
            {
                var imgDefault = string.Format("{0}/wwwroot/{1}", Environment.CurrentDirectory, path);
                if (!Directory.Exists(imgDefault))
                {
                    Directory.CreateDirectory(imgDefault);
                }

                var backImg = imgDefault + "/" + "background.jpg";
                Image image = PictureAddText(fullName, backImg, nr);

                return GetImageBytes(image);

            }

            else
            {
                throw new ServiceException("没有二维码Code");
            }


        }


        /// <summary>
        /// 几张图片合成
        /// </summary>
        /// <param name="img">需要合成进去的图</param>
        /// <param name="backgroundImg">背景图</param>
        /// <param name="photo">照片</param>
        /// <returns></returns>
        private static byte[] CreateImg(string img, string backgroundImg, string photo)
        {

            var imgDefault = string.Format("{0}/wwwroot/{1}", Environment.CurrentDirectory, path);
            if (!Directory.Exists(imgDefault))
            {
                Directory.CreateDirectory(imgDefault);
            }

            var backImg = imgDefault + "/" + backgroundImg;

            Image image = PictureAdd(img, backImg, photo);

            return GetImageBytes(image);
        }

        #endregion

        #region 合成图片工具

        #region 创建文字图片工具
        //定义一个方法
        /// <summary>
        /// 把文字转换成Bitmap
        /// </summary>
        /// <param name="text"></param>
        /// <param name="font"></param>
        /// <returns></returns>
        private static Bitmap TextToBitmap(string text, Font font)
        {
            Graphics g;
            Bitmap bmp;
            StringFormat format = new StringFormat(StringFormatFlags.NoClip);

            bmp = new Bitmap(1, 1);
            g = Graphics.FromImage(bmp);

            //计算绘制文字所需的区域大小（根据宽度计算长度），重新创建矩形区域绘图
            SizeF sizef = g.MeasureString(text, font, PointF.Empty, format);

            int width = (int)(sizef.Width + 10);
            int height = (int)(sizef.Height + 1);
            var rect = new Rectangle(0, 0, width, height);
            bmp.Dispose();

            bmp = new Bitmap(width, height);

            g = Graphics.FromImage(bmp);

            //使用ClearType字体功能
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            g.FillRectangle(new SolidBrush(Color.White), rect);

            g.DrawString(text, font, Brushes.Black, rect, format);


            return bmp;
        }
        #endregion

        /// <summary>
        /// 创建合成图片
        /// </summary>
        /// <param name="desc">文字内容</param>
        /// <param name="background">背景图片</param>
        /// <param name="qrCode">二维码</param>
        private static Image PictureAddText(string desc, string background, string qrCode)
        {

            Image imgBack = Image.FromFile(background);

            Graphics g = Graphics.FromImage(imgBack);

            var bitmap = TextToBitmap(desc, new Font("微软雅黑", 30));

            Image img = bitmap;

            //生成二维码图片
            //g.DrawImage(GetQcode(qrCode), new Rectangle(0, 5, 250, 230));

            g.DrawImage(img, 100, 220, 60, 30);

            GC.Collect();
            return imgBack;

        }

        /// <summary>
        /// 图片合成
        /// </summary>
        /// <param name="imgs">图片或者二维码</param>
        /// <param name="background">背景图片</param>
        /// <param name="photo">照片</param>
        /// <returns></returns>
        private static Image PictureAdd(string imgs, string background, string photo)
        {
            try
            {
                //背景图
                Image imgBack = Image.FromFile(background);

                Graphics g = Graphics.FromImage(imgBack);

                var savepath = string.Format("{0}/wwwroot/{1}", Environment.CurrentDirectory, path);

                var catUrl = savepath + "/" + "catEye.png";
                Image catImg = Image.FromFile(catUrl);

                HttpWebRequest req = null;
                HttpWebResponse resp = null;
                //请求网络路径地址
                req = (HttpWebRequest)WebRequest.Create(photo);
                // 超时时间
                req.Timeout = 5000;
                //获得请求结果
                using (resp = (System.Net.HttpWebResponse)req.GetResponse())
                {

                    using (Stream stream = resp.GetResponseStream())
                    {
                        //需要合成进去的图
                        Image defaultImg = Image.FromStream(stream);
                        g.DrawImage(defaultImg, 145, 280, 480, 240);
                    }
                }


                g.DrawImage(catImg, 38, 100);


                //g.DrawImage(GetQcode(imgs), 322, 1068, 105, 105);

                g.Dispose();
                return imgBack;
            }
            catch (Exception)
            {
                throw new ServiceException("创建猫眼合成图片失败");
            }
        }

        ///// <summary>
        ///// 合成二维码
        ///// </summary>
        ///// <param name="data"></param>
        ///// <returns></returns>
        //private static Image GetQcode(string data)
        //{
        //    QRCodeGenerator generator = new QRCodeGenerator();
        //    QRCodeData codeData = generator.CreateQrCode(data, QRCodeGenerator.ECCLevel.M, true);
        //    QRCoder.QRCode qrcode = new QRCoder.QRCode(codeData);
        //    Bitmap qrImage = qrcode.GetGraphic(10, Color.Black, Color.White, true);
        //    return qrImage;

        //}
        #endregion



        /// <summary>
        /// 图片装二进制流
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        private static byte[] GetImageBytes(Image image)
        {

            MemoryStream ms = new MemoryStream();
            byte[] imagedata = null;
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            imagedata = ms.GetBuffer();
            return imagedata;
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
