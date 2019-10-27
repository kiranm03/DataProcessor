using System;
using DataService.Controllers;
using DataService.Model;
using DataService.Workers;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DataService.Test.Controllers
{
    [TestClass]
    public class LPControllerTest
    {
        private LPController _subject;
        private readonly Mock<ILogger<LPController>> _logger = new Mock<ILogger<LPController>>();
        private readonly Mock<IDataLoader<LPData>> _dataLoader = new Mock<IDataLoader<LPData>>();
        private readonly Mock<IDataAggregator<LPData>> _dataAggregator = new Mock<IDataAggregator<LPData>>();


        [TestInitialize]
        public void SetUp()
        {
            _subject = new LPController(_dataLoader.Object, _dataAggregator.Object, _logger.Object);
        }

        [TestMethod]
        public void Get_FileNotFoundInS3_ReturnsResponseWithErrorMessage()
        {
            //Arrange
            var expected = "File not found in S3 Bucket";
            _dataLoader.Setup(x => x.Load())
                .Throws(new Exception(expected));
            //Act
            var actual = _subject.Get();
            //Assert
            Assert.AreEqual(actual.Message, expected);
            Assert.AreEqual(actual.Status, "Error");
        }

        [TestMethod]
        public void Get_FileInvalidToAggregate_ReturnsResponseWithErrorMessage()
        {
            //Arrange
            var expected = "Something went wrong while aggregating";
            var data = new Mock<LPData>().Object;
            var mockArr = new[] { data };
            _dataLoader.Setup(x => x.Load())
                .Returns(mockArr);
            _dataAggregator.Setup(x => x.Aggregate(mockArr))
                .Throws(new Exception(expected));
            //Act
            var actual = _subject.Get();
            //Assert
            Assert.AreEqual(actual.Message, expected);
            Assert.AreEqual(actual.Status, "Error");
        }

        [TestMethod]
        public void Get_ValidFile_ReturnsResponseWithAgrregatedData()
        {
            //Arrange
            var data = new Mock<LPData>().Object;
            var mockArr = new[] { data };
            var response = new Mock<EnergyResponse>().Object;
            var mockResponse = new[] { response };
            _dataLoader.Setup(x => x.Load())
                .Returns(mockArr);
            _dataAggregator.Setup(x => x.Aggregate(mockArr))
                .Returns(mockResponse);
            //Act
            var actual = _subject.Get();
            //Assert
            Assert.AreEqual(actual.EnergyResponse, mockResponse);
            Assert.AreEqual(actual.Status, "Success");
        }
    }
}
