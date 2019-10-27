using System;
using System.IO;
using System.Threading.Tasks;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.Logging;

namespace DataService.AWSServices
{
    public class S3Service : IS3Service
    {
        private readonly IAmazonS3 _client;
        private readonly ILogger<S3Service> _logger;

        public S3Service(IAmazonS3 client, ILogger<S3Service> logger)
        {
            _client = client;
            _logger = logger;
        }

        public async Task<string> GetFileDataAsync(string bucketName, string keyName)
        {
            string fileStream;
            try
            {
                var request = new GetObjectRequest
                {
                    BucketName = bucketName,
                    Key = keyName
                };

                var config = new AmazonS3Config { RegionEndpoint = RegionEndpoint.APSoutheast2 };
                var client = new AmazonS3Client(config);

                using (var response = await client.GetObjectAsync(request))
                using (var responseStream = response.ResponseStream)
                using (StreamReader reader = new StreamReader(responseStream))
                {
                    fileStream = reader.ReadToEnd();
                    
                }
            }
            catch(AmazonS3Exception ex)
            {
                _logger.LogError($"Error encountered while reading data from S3. Message: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong. Message: {ex.Message}");
                throw;
            }

            return fileStream;
        }
    }
}
