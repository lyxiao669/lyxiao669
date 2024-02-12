using System;
using System.Text.Json.Serialization;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using Juzhen.Wechat.Sdk;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using System.Collections.Generic;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Linq;
using static Applet.API.Application.WechatClient;
using System.Text;
using System.Security.Cryptography;
using Refit;

namespace Applet.API.Application
{
    #region
    public class GetAccessTokenResult
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }
        [JsonPropertyName("expires_in")]
        public long ExpiresIn { get; set; }
    }
    public class Jscode2sessionResult
    {
        [JsonPropertyName("openid")]
        public string OpenId { get; set; }
        [JsonPropertyName("session_key")]
        public string SessionKey { get; set; }
    }
    public class CheckEncryptedDataResult
    {
        [JsonPropertyName("vaild")]
        public bool Vaild { get; set; }
        [JsonPropertyName("create_time")]
        public long CreateTime { get; set; }
    }
    public class CheckEncryptedDataModel
    {
        public string EncryptedMsgHash { get; set; }
    }
    public static class IDistributedCacheExtensions
    {
        public static T GetOrAdd<T>(this IDistributedCache distributedCache, string key, TimeSpan timeSpan, Func<T> func)
            where T : class
        {
            var bytes = distributedCache.Get(key);
            if (bytes == null)
            {
                var data = func();
                bytes = JsonSerializer.SerializeToUtf8Bytes(data, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                });
                distributedCache.Set(key, bytes, new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = timeSpan
                });
            }
            return JsonSerializer.Deserialize<T>(bytes, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            });
        }

        public static async Task<T> GetOrAddAsync<T>(this IDistributedCache distributedCache, string key, TimeSpan timeSpan, Func<Task<T>> func)
            where T : class
        {
            var bytes = await distributedCache.GetAsync(key);
            if (bytes == null)
            {
                var data = await func();
                bytes = JsonSerializer.SerializeToUtf8Bytes(data, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                });
                await distributedCache.SetAsync(key, bytes, new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = timeSpan
                });
            }
            return JsonSerializer.Deserialize<T>(bytes, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            });
        }
    }
    public class WechatException : Exception
    {
        public long ErrorCode { get; private set; }

        public WechatException(string errmsg, long errcode)
            : base(errmsg)
        {
            ErrorCode = errcode;
        }
    }
    public abstract class ServiceAbstract
    {
        private readonly static JsonSerializerOptions options = new JsonSerializerOptions()
        {
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        protected static string ToUrl(Dictionary<string, object> values)
        {
            return "?" + string.Join("&", values.Select(s => $"{s.Key}={s.Value}"));
        }

        protected static string ToJson(Dictionary<string, object> values)
        {
            return JsonSerializer.Serialize(values, options);
        }
    }
    #endregion
    public class WechatClient:ServiceAbstract
    {
        readonly HttpClient _httpClient;
        readonly WechatOptions _options;
        readonly IConnectionMultiplexer _connectionMultiplexer;
        public WechatClient(HttpClient httpClient,
            IOptionsSnapshot<WechatOptions> options,
            IConnectionMultiplexer connectionMultiplexer)
        {
            _httpClient = httpClient;
            _options = options.Value;
            _connectionMultiplexer = connectionMultiplexer;
        }

        public async Task<Jscode2sessionResult> Jscode2sessionAsync(string jscode,string grant_type= "authorization_code")
        {
            string url = $"https://api.weixin.qq.com/sns/jscode2session?appid={_options.AppId}&secret={_options.AppSecret}&grant_type={grant_type}&js_code={jscode}";
            var response = await _httpClient.GetAsync(url);
            var json = await response.Content.ReadAsByteArrayAsync();
            var result = System.Text.Json.JsonSerializer.Deserialize<Jscode2sessionResult>(json);
            return result;
        }

        public  string WechatDecrypt(string encryptedData, string encryptIv, string sessionKey)
        {
            //base64解码为字节数组
            var encryptData = Convert.FromBase64String(encryptedData);
            var key = Convert.FromBase64String(sessionKey);
            var iv = Convert.FromBase64String(encryptIv);

            //创建aes对象
            var aes = Aes.Create();

            if (aes == null)
            {
                throw new InvalidOperationException("未能获取Aes算法实例");
            }
            //设置模式为CBC
            aes.Mode = CipherMode.CBC;
            //设置Key大小
            aes.KeySize = 128;
            //设置填充
            aes.Padding = PaddingMode.PKCS7;
            aes.Key = key;
            aes.IV = iv;

            //创建解密器
            var de = aes.CreateDecryptor();
            //解密数据
            var decodeByteData = de.TransformFinalBlock(encryptData, 0, encryptData.Length);
            //转换为字符串
            var data = Encoding.UTF8.GetString(decodeByteData);

            return data;
        }




        public async Task<GetAccessTokenResult> GetAccessTokenAsync()
        {
            var key = $"AccessToken:{_options.AppId}";
            var builder = new Dictionary<string, object>()
                {
                    { "grant_type", "client_credential"},
                    { "appid", _options.AppId},
                    { "secret", _options.AppSecret},
                };
            var url = "https://api.weixin.qq.com/cgi-bin/token" + ToUrl(builder);
            var response = await _httpClient.GetAsync(url);
            var bytes = await response.Content.ReadAsByteArrayAsync();
            var result= JsonSerializer.Deserialize<GetAccessTokenResult>(bytes);
            return result;
        }

        public async Task<CheckEncryptedDataResult> CheckEncryptedData(CheckEncryptedDataModel model)
        {
            var token = await GetAccessTokenAsync();
            var query = new Dictionary<string, object>()
            {
                { "access_token", token.AccessToken},
            };
            var url = "https://api.weixin.qq.com/wxa/business/checkencryptedmsg" + ToUrl(query);
            var json = new Dictionary<string, object>()
            {
                { "encrypted_msg_hash",model.EncryptedMsgHash},
            };
            var content = new StringContent(ToJson(json), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(url, content);
            var bytes = await response.Content.ReadAsByteArrayAsync();
            var result = JsonSerializer.Deserialize<CheckEncryptedDataResult>(bytes);
            return result;
        }



    }
}
