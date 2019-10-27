using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.S3;
using DataService.AWSServices;
using DataService.Mappers;
using DataService.Model;
using DataService.Workers;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TinyCsvParser.Mapping;

namespace DataService.Test.Workers
{
    [TestClass]
    public class TOUDataLoaderTest
    {
        private TOUDataLoader _subject;
        private readonly Mock<ILogger<TOUDataLoader>> _logger = new Mock<ILogger<TOUDataLoader>>();
        private readonly ICsvMapping<TOUData> _csvMapping = new CsvTOUDataMapping();
        private readonly Mock<IS3Service> _s3Service = new Mock<IS3Service>();

        [TestInitialize]
        public void SetUp()
        {
            _subject = new TOUDataLoader(_csvMapping, _s3Service.Object, _logger.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(AmazonS3Exception))]
        public void Load_S3AccessDenied_ThrowsException()
        {
            //Arrange
            _s3Service.Setup(x => x.GetFileDataAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Throws(new AmazonS3Exception("Access Denied"));
            //Act
            _subject.Load();
            //Assert
        }

        [TestMethod]
        public void Load_ValidFile_ReturnsTOUData()
        {
            //Arrange
            var source = new StringBuilder()
                .AppendLine("MeterCode,Serial,PlantCode,DateTime,Quality,Stream,DataType,Energy,Units")
                .AppendLine("214667141,214667141,ED071500133,11/09/2018 10:00,A,B1,Import Wh Total,123,kwh")
                .AppendLine("14667141,214667141,ED071500133,11/09/2018 4:30,A,B1,Import Wh Total,142.3,kwh")
                .ToString();

            _s3Service.Setup(x => x.GetFileDataAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.FromResult(source));
            //Act
            var actual = _subject.Load();
            //Assert
            Assert.AreEqual(actual.Count(), 2);
            Assert.IsInstanceOfType(actual.First(), typeof(TOUData));
        }
    }
}
