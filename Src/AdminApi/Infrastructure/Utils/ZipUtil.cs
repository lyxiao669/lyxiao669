using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
using System.Net;
using System.Text;
using System.Web;

namespace Infrastructure
{
    /// <summary>
    /// 压缩本地文件
    /// </summary>
    public class ZipUtil
    {


        public static Stream Download(List<string> urlStr)
        {


            //使用WebClient 下载文件
            System.Net.WebClient myWebClient = new System.Net.WebClient();

            //存 文件名 和 数据流
            Dictionary<string, Stream> dc = new Dictionary<string, Stream>();

            //取出字符串中信息 (文件名和地址)
            for (int i = 0; i < urlStr.Count; i++)
            {
                //使用 ',' 分隔 文件名和路径 [0]位置是文件名, [1] 位置是路径
                string[] urlSp = urlStr[i].Split(',');

                //调用WebClient 的 DownLoadData 方法 下载文件
                byte[] data = myWebClient.DownloadData(urlSp[1]);
                Stream stream = new MemoryStream(data);//byte[] 转换成 流

                //放入 文件名 和 stream
                dc.Add(urlSp[0] + ".jpg", stream);//这里指定为 .jpg格式 (自己可以随时改)
            }

            //调用压缩方法 进行压缩 (接收byte[] 数据)
            byte[] fileBytes = ConvertZipStream(dc);

            Stream result;
            try
            {
                Stream stream=new MemoryStream(fileBytes);
                result = stream;
            }
            catch
            {
                throw null;
            }

            return result;

        }




        public static byte[] ConvertZipStream(Dictionary<string, Stream> streams)
        {
            Encoding code = Encoding.GetEncoding("utf-8");

            ZipConstants.DefaultCodePage = code.CodePage;
            byte[] buffer = new byte[6500];
            MemoryStream returnStream = new MemoryStream();
            var zipMs = new MemoryStream();
            using (ICSharpCode.SharpZipLib.Zip.ZipOutputStream zipStream = new ICSharpCode.SharpZipLib.Zip.ZipOutputStream(zipMs))
            {
                zipStream.SetLevel(9);//设置 压缩等级 (9级 500KB 压缩成了96KB)
                foreach (var kv in streams)
                {
                    string fileName = kv.Key.Split("/")[1];
                    using (var streamInput = kv.Value)
                    {
                        zipStream.PutNextEntry(new ICSharpCode.SharpZipLib.Zip.ZipEntry(fileName));
                        while (true)
                        {
                            var readCount = streamInput.Read(buffer, 0, buffer.Length);
                            if (readCount > 0)
                            {
                                zipStream.Write(buffer, 0, readCount);
                            }
                            else
                            {
                                break;
                            }
                        }
                        zipStream.Flush();
                    }
                }
                zipStream.Finish();
                zipMs.Position = 0;
                zipMs.CopyTo(returnStream, 5600);
            }
            returnStream.Position = 0;

            //Stream转Byte[]
            byte[] returnBytes = new byte[returnStream.Length];
            returnStream.Read(returnBytes, 0, returnBytes.Length);
            returnStream.Seek(0, SeekOrigin.Begin);

            return returnBytes;

        }
    }
}
