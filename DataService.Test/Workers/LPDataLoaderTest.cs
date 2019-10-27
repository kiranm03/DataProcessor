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
    public class LPDataLoaderTest
    {
        private LPDataLoader _subject;
        private readonly Mock<ILogger<LPDataLoader>> _logger = new Mock<ILogger<LPDataLoader>>();
        private readonly ICsvMapping<LPData> _csvMapping = new CsvLPDataMapping();
        private readonly Mock<IS3Service> _s3Service = new Mock<IS3Service>();

        [TestInitialize]
        public void SetUp()
        {
            _subject = new LPDataLoader(_csvMapping, _s3Service.Object, _logger.Object);
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
        public void Load_ValidFile_ReturnsLPData()
        {
            //Arrange
            var source = new StringBuilder()
                .AppendLine("MeterPoint Code,Serial Number,Plant Code,Date/Time,Data Type,Data Value,Units,Status")
                .AppendLine("214612653,214612653,ED021401463,07/09/2018 14:45:00,Phase Angle B,23.630000,deg,GN")
                .AppendLine("214612653,214612653,ED021401463,07/09/2018 14:50:00,Phase Angle B,32.590000,deg,")
                .ToString();

            _s3Service.Setup(x => x.GetFileDataAsync(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(Task.FromResult(source));
            //Act
            var actual = _subject.Load();
            //Assert
            Assert.AreEqual(actual.Count(), 2);
            Assert.IsInstanceOfType(actual.First(), typeof(LPData));
        }
    }
}
