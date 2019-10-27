using System;
using System.IO;
using System.Threading.Tasks;

namespace DataService.AWSServices
{
    public interface IS3Service
    {
        Task<string> GetFileDataAsync(string bucketName, string keyName);
    }
}
