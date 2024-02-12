using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using System;
using System.Linq;
using System.Text;

namespace Juzhen.Infrastructure
{
    /// <summary>
    /// 数据转换工具类
    /// </summary>
    public static class ConvertUtil
    {

        #region json
        /// <summary>
        /// 将对象转换成JSON字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJson(this object obj)
        {
            var serializerSettings = new JsonSerializerSettings
            {
                Converters =
                {
                    //日期处理
                    new IsoDateTimeConverter()
                     {
                        DateTimeFormat= "yyyy-MM-dd HH:mm:ss"
                     }
                },
                //Camel命名:首字母小写
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                StringEscapeHandling = StringEscapeHandling.EscapeNonAscii
            };
            return JsonConvert.SerializeObject(obj, serializerSettings);
        }
        /// <summary>
        /// 将JSON字符串转换成指定模型
        /// 要求C#字段名首字母大小，js属性名首字母小写
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T FromJson<T>(this string json)
        {
           
                return (T)JsonConvert.DeserializeObject(json, typeof(T), new JsonSerializerSettings()
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                });
            
        }
        /// <summary>
        /// 将json反序列化成匿名类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static T FromJson<T>(this string json, Func<T> func)
        {
            try
            {
                return JsonConvert.DeserializeAnonymousType(json, func(), new JsonSerializerSettings()
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                });
            }
            catch
            {
                return default;
            }
        }
        /// <summary>
        /// 将JSON字符串转换成JSON对象
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static object FromJson(this string json)
        {
            try
            {
                var obj = JsonConvert.DeserializeObject(json);
                return obj ?? new object();
            }
            catch
            {
                return new object();
            }
        }
        /// <summary>
        /// 将JSON字符串转换成JSON对象
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static JObject FromJsonObject(this string json)
        {
            try
            {
                var obj = JsonConvert.DeserializeObject(json) as JObject;
                return obj ?? new JObject();
            }
            catch
            {
                return new JObject();
            }
        }
        /// <summary>
        /// 将JSON字符串转换成JSON数组
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static JArray FromJsonArray(this string json)
        {
            try
            {
                var obj = JsonConvert.DeserializeObject(json) as JArray;
                return obj ?? new JArray();
            }
            catch
            {
                return new JArray();
            }
        }
        #endregion

        #region value type
        public static bool Contains(this ValueType value, params ValueType[] enumerable)
        {
            return enumerable.Contains(value);
        }
        #endregion

        #region string
        /// <summary>
        /// 转化成Base64字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToBase64(this string str)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(str));
        }
        /// <summary>
        /// 解析Base64字符
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string FromBase64(this string str)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(str));
        }
        /// <summary>
        /// 转化成byte数组
        /// </summary>
        /// <param name="str"></param>
        /// <param name="encoding">默认是UTF-8</param>
        /// <returns></returns>
        public static byte[] ToBytes(this string str, string encoding = "UTF-8")
        {
            return Encoding.GetEncoding(encoding).GetBytes(str);
        }
        /// <summary>
        /// 将字符编码从一种转换成另一种
        /// </summary>
        /// <param name="str"></param>
        /// <param name="srcEncoding"></param>
        /// <param name="dstEncoding"></param>
        /// <returns></returns>
        public static string EncodingConvert(this string str, string srcEncoding = "UTF-8", string dstEncoding = "GBK")
        {
            return Encoding.GetEncoding(dstEncoding).GetString(Encoding.Convert(Encoding.GetEncoding(srcEncoding), Encoding.GetEncoding(dstEncoding), Encoding.GetEncoding(srcEncoding).GetBytes(str)));
        }
        ///// <summary>
        ///// 转化成MD5-32bit字符串
        ///// </summary>
        ///// <param name="str"></param>
        ///// <returns></returns>
        //public static string ToMd5_32Bit(this string str)
        //{
        //    return SecurityUtil.Md5_32BitHash(str);
        //}
        ///// <summary>
        ///// 转化成MD5-16bit字符串
        ///// </summary>
        ///// <param name="str"></param>
        ///// <returns></returns>
        //public static string ToMd5_16Bit(this string str)
        //{
        //    return SecurityUtil.Md5_16BitHash(str);
        //}
        #endregion

        #region Enum
        public static int ToInt(this Enum obj)
        {
            return Convert.ToInt32(obj);
        }
        #endregion

        //#region date
        ///// <summary>
        ///// 转换成时间戳
        ///// </summary>
        ///// <param name="datetime"></param>
        ///// <returns></returns>
        //public static long ToTimestamp(this DateTime datetime)
        //{
        //    return TimeUtil.ToTimestamp(datetime);
        //}
        ///// <summary>
        ///// 转换成时间
        ///// </summary>
        ///// <param name="timestamp"></param>
        ///// <returns></returns>
        //public static DateTime FromTimestamp(this long timestamp)
        //{
        //    return TimeUtil.FromTimestamp(timestamp);
        //}
        //#endregion

    }
   
}
