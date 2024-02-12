using Juzhen.Wechat.Sdk;
using Mammothcode.Wechat;
using System.IO;
using System.Net;

namespace Juzhen.MiniProgramAPI
{
    public class SecCheckUtil
    {
        
        public static string ImgSecCheck(string cenTent,string accessToken)
        {
           
            var keys = new JsonBuilder();
            keys.Add("access_token", accessToken);
            keys.Add("media_url", cenTent);
            keys.Add("media_type", 2);

            string url = $"https://api.weixin.qq.com/wxa/img_sec_check?access_token={accessToken}";

            HttpWebRequest request = WebRequest.Create(cenTent) as HttpWebRequest;
            //获取WebResponse对象
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            //直到request.GetResponse()程序才开始向目标网页发送Post请求
            //关键：获取Stream对象 (http请求的文件流对象)
            Stream responseStream = response.GetResponseStream();

           
            BufferedStream buffered = new BufferedStream(responseStream);
            string data = "{\"media\":[{\"contentType\": "+ response.ContentType + ",\"value\":" + cenTent + "}]}";
            string xml = HttpUtil.Post(new Mammothcode.Wechat.HttpItem()
            {
                Method = "POST",
                URL = url,
                ContentType= "application/octet-stream",
                Data = data
            }).Document;
            return xml;
        }

        public static string msgSecCheck(string cenTent, string accessToken)
        {
            
            //string url = $"https://api.weixin.qq.com/wxa/msg_sec_check?access_token={accessToken}&content={cenTent}";
            string xml = HttpUtil.Post(new Mammothcode.Wechat.HttpItem()
            {
                Method = "POST",
                URL = "https://api.weixin.qq.com/wxa/msg_sec_check?access_token=" + accessToken,
                ContentType = "application/json",
                Data = "{\"content\":\"" + cenTent + "\"}"
            }).Document;
            return xml;
        }
    }
}
