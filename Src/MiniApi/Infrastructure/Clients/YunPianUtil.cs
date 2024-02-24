using Mammothcode.Wechat;

namespace Juzhen.MiniProgramAPI
{
    /// <summary>
    /// 云片短信验证
    /// </summary>
    public static class YunPianUtil
    {
        readonly static string _apikey = "1b9d81b7b68a679a005f491907cbeabf";
        public static string SendSingle(string mobile, string text)
        {
            var url = string.Format("https://sms.yunpian.com/v2/sms/single_send.json?apikey={0}&mobile={1}&text={2}", _apikey, mobile, text);

            var res = HttpUtil.Post(new HttpItem()
            {
                URL = url,
                Accept = "application/json;charset=utf-8;",
                ContentType = "application/x-www-form-urlencoded;charset=utf-8;",
                Method = "POST",
            });
            return res.Document;
        }
    }
}
