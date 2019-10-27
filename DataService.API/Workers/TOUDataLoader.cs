using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DataService.AWSServices;
using DataService.Model;
using DataService.Util;
using Microsoft.Extensions.Logging;
using TinyCsvParser;
using TinyCsvParser.Mapping;

namespace DataService.Workers
{
    public class TOUDataLoader : IDataLoader<TOUData>
    {
        private readonly ICsvMapping<TOUData> _csvMapping;
        private readonly IS3Service _s3Service;
        private readonly ILogger<TOUDataLoader> _logger;
        private readonly string _bucketName = Environment.GetEnvironmentVariable(Constants.S3_BUCKET_NAME);
        private readonly string _fileName = Environment.GetEnvironmentVariable(Constants.TOU_FILE_NAME);

        public TOUDataLoader(ICsvMapping<TOUData> csvMapping, IS3Service s3Service, ILogger<TOUDataLoader> logger)
        {
            _csvMapping = csvMapping;
            _s3Service = s3Service;
            _logger = logger;
        }

        public IEnumerable<TOUData> Load()
        {
            try
            {
                CsvParserOptions csv = new CsvParserOptions(true, ',');
                var csvParser = new CsvParser<TOUData>(csv, _csvMapping);
                var csvReaderOptions = new CsvReaderOptions(new[] { Environment.NewLine });
                var stream = _s3Service.GetFileDataAsync(_bucketName, _fileName).Result;
                var records = csvParser.ReadFromString(csvReaderOptions, stream);

                return records
                    .Select(x => x.Result);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Something went wrongn while loading the file data:: {ex.Message}");
                throw;
            }
        }
    }
}
