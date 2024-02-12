using System;
using Minio;
using Minio.Exceptions;
using Minio.DataModel;
using Minio.Credentials;
using Minio.DataModel.Args;
using System.Threading.Tasks;

namespace AdminApi.Infrastructure.Utils
{
    public class MinioUtil
    {
        public static string Endpoint = "play.min.io";
        public static string AccessKey = "Q3AM3UQ867SPQQA43P2F";
        public static string SecretKey = "zuf+tfteSlswRu7BJ86wtrueekitnifILbZam1KYY3TG";

        public static void AddConfigSource(string endpoint, string accessKey, string secretKey)
        {
            Endpoint = endpoint;
            AccessKey = accessKey;
            SecretKey = secretKey;
            try
            {
                var minio = new MinioClient()
                                    .WithEndpoint(endpoint)
                                    .WithCredentials(accessKey, secretKey)
                                    .WithSSL()
                                    .Build();
                //Run(minio).Wait();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
        private async static Task Run(MinioClient minio)
        {
            var bucketName = "mymusic";
            var location = "us-east-1";
            var objectName = "golden-oldies.zip";
            var filePath = "C:\\Users\\username\\Downloads\\golden_oldies.mp3";
            var contentType = "application/zip";

            try
            {
                // Make a bucket on the server, if not already present.
                var beArgs = new BucketExistsArgs()
                    .WithBucket(bucketName);
                bool found = await minio.BucketExistsAsync(beArgs).ConfigureAwait(false);
                if (!found)
                {
                    var mbArgs = new MakeBucketArgs()
                        .WithBucket(bucketName);
                    await minio.MakeBucketAsync(mbArgs).ConfigureAwait(false);
                }
                // Upload a file to bucket.
                var putObjectArgs = new PutObjectArgs()
                    .WithBucket(bucketName)
                    .WithObject(objectName)
                    .WithFileName(filePath)
                    .WithContentType(contentType);
                await minio.PutObjectAsync(putObjectArgs).ConfigureAwait(false);
                Console.WriteLine("Successfully uploaded " + objectName);
            }
            catch (MinioException e)
            {
                Console.WriteLine("File Upload Error: {0}", e.Message);
            }
        }
    }
}

