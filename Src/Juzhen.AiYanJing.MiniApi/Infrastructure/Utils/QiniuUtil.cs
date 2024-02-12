using Qiniu.Http;
using Qiniu.Storage;
using Qiniu.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Juzhen.Qiniu.Infrastructure
{
    public class QiniuUtil
    {


        public static string AccessKey = "";
        public static string SecretKey = "";
        public static string Domain = "";
        public static string Scope = "";


        public static void AddConfigSource(string accessKey, string secretKey, string domain, string scope)
        {
            AccessKey = accessKey;
            SecretKey = secretKey;
            Domain = domain;
            Scope = scope;
        }

        /// <summary>
        /// 获取凭证
        /// </summary>
        /// <returns></returns>
        public static string GetUploadToken()
        {
            Mac mac = new Mac(AccessKey, SecretKey);
            PutPolicy putPolicy = new PutPolicy();
            //空间域
            putPolicy.Scope = Scope;
            //token的有效时间
            putPolicy.SetExpires(3600);
            //指定多少天自动删除
            //putPolicy.DeleteAfterDays = 1;
            return Auth.CreateUploadToken(mac, putPolicy.ToJsonString());


        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string GetUploadFile(byte[] filePath)
        {
            var token = GetUploadToken();
            Guid guid = Guid.NewGuid();
            var key = guid.ToString();
            Config config = new Config()
            {
                Zone = Zone.ZONE_CN_East,
                ChunkSize = ChunkUnit.U4096K,
                UseHttps = true,
                UseCdnDomains = true,
            };

            #region 上传
            FormUploader target = new FormUploader(config);
            HttpResult result = target.UploadData(filePath, key, token, null);
            #endregion
            #region 拿到图片
            String publicUrl = "https://" + Domain + "/" + key;
            return publicUrl;
            #endregion
        }

        public string ResumableUploader(Stream stream)
        {
            var token = GetUploadToken();
            Config config = new Config()
            {
                Zone = Zone.ZONE_CN_East,
                ChunkSize = ChunkUnit.U4096K,
                UseHttps = true,
                UseCdnDomains = true,
            };

            var uploader = new ResumableUploader(new Config() { });
            return null;//uploader.UploadFile(stream,"",token,null).Result.Text;
        }
    }
}